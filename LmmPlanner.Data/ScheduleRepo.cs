using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LmmPlanner.Entities.Models;
using LmmPlanner.Data.Helper;
using Microsoft.EntityFrameworkCore;
using LmmPlanner.Entities.Interfaces;
using LmmPlanner.Entities.Enums;
using AutoMapper;

namespace LmmPlanner.Data
{
    public class ScheduleRepo : IScheduleRepo
    {
        private IMapper _mapper;
        private MyContext db;
        public ScheduleRepo(MyContext context,
        IMapper mapper)
        {
            _mapper = mapper;
            db = context;
        }

        public async Task<MeetingInfo> GetSchedule(DateTime now)
        {
            DateTime myTimeStart = now.DayOfWeek == DayOfWeek.Monday ? now : now.AddDays(-1 * ((int)now.DayOfWeek - 1));
            DateTime myTimeEnd = myTimeStart.AddDays(6);
            MeetingInfo? activeSchedule = await db.LmmMeetings
            .Where(lm => lm.Date >= myTimeStart.AddDays(-1) && lm.Date <= myTimeEnd && lm.Active == true)
            .OrderBy(d => d.Date)
            .Select(d => new MeetingInfo
            {
                MeetingId = d.Id,
                MeetingDate = d.Date,
                BibleReading = d.BibleReading,
                ChairmanId = d.Chairman,
                Chairman = d.ChairmanPerson != null ? d.ChairmanPerson.Firstname + " " + d.ChairmanPerson.Lastname : "",
                SongBeginning = d.SongBeginning,
                SongMiddle = d.SongMiddle,
                SongEnd = d.SongEnd,
                ClosingComments = d.ClosingComments,
                PrayerBeginningId = d.PrayerBeginning,
                PrayerBeginning = d.PrayerBeginningPerson != null ? d.PrayerBeginningPerson.Firstname + " " + d.PrayerBeginningPerson.Lastname : "",
                PrayerEndId = d.PrayerEnd,
                PrayerEnd = d.PrayerEndPerson != null ? d.PrayerEndPerson.Firstname + " " + d.PrayerEndPerson.Lastname : "",
            }).FirstOrDefaultAsync();
            if (activeSchedule == null)
            {
                activeSchedule = new() { MeetingDate = myTimeStart };
            }
            List<MeetingPartInfo> parts = await db.LmmSchedules
            .Where(d => d.Date >= myTimeStart.AddDays(-1) && d.Date <= myTimeEnd)
            .OrderBy(d => d.Roworder)
            .Select(d => new MeetingPartInfo
            {
                Id = d.Id,
                Theme = d.Theme,
                TalkId = d.TalkId,
                Source = d.Source,
                Time = d.Time,
                RowOrder = d.Roworder,
                TalkMeeting = d.TalkInfo != null ? d.TalkInfo.Meeting : null,
                TalkMeetingSection = d.TalkInfo != null ? d.TalkInfo.MeetingSection : null
            })
            .ToListAsync();

            List<long?> schedIds = parts.Select(d => (long?)d.Id).ToList();
            List<MeetingAssignmentInfo> asmnt = await db.LmmAssignments
            .Where(d => schedIds.Contains(d.LmmScheduleId))
            .Select(d => new MeetingAssignmentInfo
            {
                ScheduleId = d.LmmScheduleId,
                AssignmentId = d.Id,
                Classnumber = d.Classnumber == null ? 0 : d.Classnumber,
                MainPerson = d.MainPerson == null ? "" : d.MainPerson.Firstname + " " + d.MainPerson.Lastname,
                AssistantPerson = d.AssistantPerson == null ? "" : d.AssistantPerson.Firstname + " " + d.AssistantPerson.Lastname
            })
            .ToListAsync();
            activeSchedule.AssignmentInfos = asmnt;
            activeSchedule.PartInfos = parts;

            List<TheocData.Exception> exeption = await db.Exceptions.Where(d => d.Active == true && d.Date >= myTimeStart.AddDays(-1) && d.Date <= myTimeEnd).ToListAsync();
            if (exeption.Any())
            {
                var exc = exeption.First();
                activeSchedule.ExceptionVariant = (ExceptionVariant)(exc.Type ?? 6); // ?? ExceptionVariant.Unknown ;
                if (exc.Date != exc.Date2)
                {
                    activeSchedule.Alert = $"{exc.Date:dd.MM.yyyy} - {exc.Date2:dd.MM.yyyy} - {ExceptionHelper.TypeToString(exc.Type)}";
                }
                else
                {
                    activeSchedule.Alert = $"{exc.Date:dd.MM.yyyy} - {ExceptionHelper.TypeToString(exc.Type)}";
                }
            }
            activeSchedule.CircuitOverseer = db.Settings.First(c => c.Name == "circuitoverseer").Value;
            return activeSchedule;
        }

        public async Task<FittingPersons> GetPersonsToPart(long partId, bool assist)
        {
            TheocData.LmmSchedule sched = await db.LmmSchedules.Where(d => d.Id == partId)
            .FirstAsync();
            FittingPersons fit = new()
            {
                ScheduleId = partId,
                Theme = sched.Theme,
                TalkId = sched.TalkId,
                Assist = assist,
                PartInfo = MeetingPartInfo.GetPartType(sched.TalkId)
            };
            List<LmmPerson> persons = await new DataRepo(db, _mapper).GetAllPersonsForDate(sched.Date);

            List<LmmPerson> partner = persons.Where(d => d.IsPartner).ToList();
            bool addPartner = false;
            switch (fit.PartInfo)
            {
                case PartType.Chair:
                    fit.Persons = persons.Where(d => d.IsLmmChair).ToList();
                    break;
                case PartType.FirstTalk:
                    fit.Persons = persons.Where(d => d.IsLmmTalk).ToList();
                    break;
                case PartType.Treasures:
                    fit.Persons = persons.Where(d => d.IsTreasurePart).ToList();
                    break;
                case PartType.BibleReading:
                    fit.Persons = persons.Where(d => d.IsBibleReader).ToList();
                    break;
                case PartType.InitialCall:
                    fit.Persons = persons.Where(d => d.IsInitialCall).ToList();
                    addPartner = true;
                    break;
                case PartType.ReturnVisit:
                    fit.Persons = persons.Where(d => d.IsReturnVisit).ToList();
                    addPartner = true;
                    break;
                case PartType.BibleStudy:
                    fit.Persons = persons.Where(d => d.IsBibleStudy).ToList();
                    addPartner = true;
                    break;
                case PartType.Talk:
                    fit.Persons = persons.Where(d => d.IsStudyTalk).ToList();
                    break;
                case PartType.LifePart:
                    fit.Persons = persons.Where(d => d.IsVideoPart).ToList();
                    break;
                case PartType.CongregationStudy:
                    fit.Persons = persons.Where(d => d.IsCongregationStudy).ToList();
                    if (assist)
                    {
                        fit.Persons = persons.Where(d => d.IsCongregationStudyReader).ToList();
                        assist = false;
                    }
                    break;
                default:
                    fit.Persons = persons;
                    break;
            }
            List<long> personIds = fit.Persons.Select(p => p.Id).ToList();
            if (assist)
            {
                if (addPartner)
                {
                    // dont add twice
                    fit.Persons.AddRange(partner.Where(p => !personIds.Contains(p.Id)));
                }
                var assign = db.LmmAssignments.Where(d => d.LmmScheduleId == sched.Id)
                .Select(a => new { Gender = a.MainPerson == null ? "" : a.MainPerson.Gender }).FirstOrDefault();
                if (assign != null)
                {
                    fit.Persons = fit.Persons.Where(p => p.Gender == assign.Gender).ToList();
                }
            }
            else
            {

            }

            fit.Persons = fit.Persons.OrderBy(d => d.LastAssignment).ToList();
            return fit;
        }
    }
}