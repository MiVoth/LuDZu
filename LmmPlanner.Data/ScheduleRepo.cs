using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LmmPlanner.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LmmPlanner.Data
{
    public class ScheduleRepo
    {
        private MyContext db;
        public ScheduleRepo(MyContext context)
        {
            db = context;
        }

        public async Task<MeetingInfo> GetSchedule(DateTime now)
        {
            var myTime = (int)now.DayOfWeek == 1 ? now : now.AddDays(-1 * (int)now.DayOfWeek);
            var schd = await db.LmmMeetings
            .Where(lm => lm.Date >= myTime.AddDays(-1) && lm.Date <= myTime.AddDays(6))
            .OrderBy(d => d.Date)
            .Select(d => new MeetingInfo
            {
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
            }).FirstAsync();

            var parts = await db.LmmSchedules
            .Where(d => d.Date >= myTime.AddDays(-1) && d.Date <= myTime.AddDays(6))
            .OrderBy(d => d.Roworder)
            .Select(d => new MeetingPartInfo
            {
                Id = d.Id,
                Theme = d.Theme,
                TalkId = d.TalkId,
                Source = d.Source,
                Time = d.Time,
                TalkMeeting = d.TalkInfo != null ? d.TalkInfo.Meeting : null,
                TalkMeetingSection = d.TalkInfo != null ? d.TalkInfo.MeetingSection : null
            })
            .ToListAsync();

            var schedIds = parts.Select(d => (long?)d.Id).ToList();
            var asmnt = await db.LmmAssignments
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
            schd.AssignmentInfos = asmnt;
            schd.PartInfos = parts;
            return schd;
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
            List<LmmPerson> persons = await new DataRepo(db).GetAllPersonsForDate(sched.Date ?? DateTime.Now);

            List<LmmPerson> partner = persons.Where(d => d.IsPartner).ToList();
            bool addPartner = false;
            switch (fit.PartInfo)
            {
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
                    fit.Persons = persons.Where(d => d.IsLmmTalk).ToList();
                    break;
                case PartType.LifePart:
                    fit.Persons = persons.Where(d => d.IsVideoPart).ToList();
                    break;
                case PartType.CongregationStudy:
                    fit.Persons = persons.Where(d => d.IsCongregationStudy).ToList();
                    break;
                default:
                    fit.Persons = persons;
                    break;
            }
            if (assist)
            {
                var assign = db.LmmAssignments.Where(d => d.LmmScheduleId == sched.Id)
                .Select(a => new { Gender = a.MainPerson == null ? "" : a.MainPerson.Gender }).FirstOrDefault();
                if (assign != null)
                {
                    fit.Persons = fit.Persons.Where(p => p.Gender == assign.Gender).ToList();
                }
                if (addPartner)
                {
                    fit.Persons.AddRange(partner);
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