using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using LmmPlanner.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LmmPlanner.Data.Statistics
{
    public static class Extension
    {
        public static int Week(this DateTime date)
        {
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
    public class ChairStatisticsRepo : IChairStatisticsRepo
    {
        private MyContext ctx;

        public ChairStatisticsRepo(MyContext context)
        {
            ctx = context;
        }

        public async Task<List<AssignmentOverviewMeeting>> GetCompleteOverview(DateTime from, DateTime to, List<long?> personIds)
        {
            var meetings = await ctx.LmmMeetings.Where(d => d.Date > from && d.Date < to)
                .OrderByDescending(m => m.Date)
                .Select(s => new AssignmentOverviewMeeting
                {
                    LmmMeetingId = s.Id,
                    Date = s.Date,
                    Chairman = s.Chairman,
                    PrayerBeginning = s.PrayerBeginning,
                    PrayerEnd = s.PrayerEnd
                })
                .ToListAsync();

            var studys = await ctx.LmmSchedules.Where(d => d.Date > from && d.Date < to)
                .Where(d => d.Assignments.Any(d => personIds.Contains(d.AssigneeId) || personIds.Contains(d.AssistantId)))
                .Select(d => new AssignmentOverviewSchedule
                {
                    Date = d.Date,
                    TalkId = d.TalkId,
                    PersonId = d.Assignments.Select(d => d.AssigneeId).FirstOrDefault(),
                    AssistantId = d.Assignments.Select(d => d.AssistantId).FirstOrDefault()
                }).ToListAsync();

            foreach (var stud in studys)
            {
                stud.PartTypeName = PartTypeStatics.PartTypeList.FirstOrDefault(d => d.IsBetween(stud.TalkId ?? 0))?.Kuerzel ?? "--";
            }
            studys.AddRange(meetings.Select(d => new AssignmentOverviewSchedule
            {
                Date = d.Date,
                PersonId = d.Chairman,
                PartTypeName = PartTypeStatics.Chair.Kuerzel
            }).ToList());
            studys.AddRange(meetings.Select(d => new AssignmentOverviewSchedule
            {
                Date = d.Date,
                PersonId = d.PrayerBeginning,
                PartTypeName = PartTypeStatics.Prayer.Kuerzel
            }).ToList());
            studys.AddRange(meetings.Select(d => new AssignmentOverviewSchedule
            {
                Date = d.Date,
                PersonId = d.PrayerEnd,
                PartTypeName = PartTypeStatics.Prayer.Kuerzel
            }).ToList());
            foreach (var item in meetings)
            {
                var study = studys.Where(d => d.Date == item.Date).ToList();
                item.Schedules = study;
            }
            return meetings;
        }

        public async Task<List<PartOverviewMeeting>> GetPartOverview(DateTime from, DateTime to, PartType partType)
        {
            var searchPart = PartTypeStatics.PartTypeList.First(d => d.TypeValue == partType);

            var myExc = await ctx.Exceptions.Where(d => d.Date > from && d.Date < to).ToListAsync();
            var meetings = await ctx.LmmMeetings.Where(d => d.Date > from && d.Date < to)
                .OrderByDescending(m => m.Date)
                .Select(s => new PartOverviewMeeting()
                {
                    LmmMeetingId = s.Id,
                    Date = s.Date,
                    Chairman = s.Chairman,
                    PrayerBeginnung = s.PrayerBeginning,
                    PrayerEnd = s.PrayerEnd
                })
                .ToListAsync();
            List<PartOverviewSchedule> studys = new();
            if (partType == PartType.Chair)
            {
                studys = meetings.Select(d => new PartOverviewSchedule
                {
                    Id = 0,
                    Date = d.Date,
                    AssigneeId = d.Chairman
                }).ToList();
            }
            else if (partType == PartType.Prayer)
            {
                studys = meetings.Select(d => new PartOverviewSchedule
                {
                    Id = 0,
                    Date = d.Date,
                    AssigneeId = d.PrayerBeginnung,
                    AssigneeId2 = d.PrayerEnd
                }).ToList();
            }
            else
            {
                studys = await ctx.LmmSchedules.Where(d => d.Date > from && d.Date < to)
                .Where(d => d.TalkId >= searchPart.TypeIdMin && d.TalkId < searchPart.TypeIdMax)
                .Select(d => new PartOverviewSchedule
                {
                    Id = d.Id,
                    Date = d.Date,
                    AssigneeId = d.Assignments.FirstOrDefault() == null ? null : d.Assignments.First().AssigneeId
                }).ToListAsync();
            }
            foreach (var item in meetings)
            {
                var study = studys.SingleOrDefault(d => d.Date == item.Date);
                item.Schedule = study ?? new();
            }

            return meetings;
        }

        public async Task<List<ChairOverview>> GetChairOverview(DateTime from, DateTime to)
        {

            var myExc = await ctx.Exceptions.Where(d => d.Date > from && d.Date < to).ToListAsync();


            var meetings = await ctx.LmmMeetings.Where(d => d.Date > from && d.Date < to)
                .OrderByDescending(m => m.Date)
                .Select(s => new ChairOverview()
                {
                    LmmMeetingId = s.Id,
                    Date = s.Date,
                    ChairmanId = s.Chairman,
                    Chairman = s.ChairmanPerson == null ? "" : s.ChairmanPerson.Firstname + " " + s.ChairmanPerson.Lastname,
                    PrayerBeginning = s.PrayerBeginningPerson == null ? "" : s.PrayerBeginningPerson.Firstname + " " + s.PrayerBeginningPerson.Lastname,
                    PrayerEnd = s.PrayerEndPerson == null ? "" : s.PrayerEndPerson.Firstname + " " + s.PrayerEndPerson.Lastname
                })
                .ToListAsync();
            var studys = await ctx.LmmSchedules.Where(d => d.Date > from && d.Date < to)
            .Where(d => d.TalkId >= 140 && d.TalkId < 160)
            .Select(d => new
            {
                d.Id,
                d.Date,
                MainFirst = d.Assignments.Select(f => f.MainPerson != null ? f.MainPerson.Firstname : "").FirstOrDefault(),
                MainLast = d.Assignments.Select(f => f.MainPerson != null ? f.MainPerson.Lastname : "").FirstOrDefault(),
                VolunteerFirst = d.Assignments.Select(f => f.AssistantPerson != null ? f.AssistantPerson.Firstname : "").FirstOrDefault(),
                VolunteerLast = d.Assignments.Select(f => f.AssistantPerson != null ? f.AssistantPerson.Lastname : "").FirstOrDefault(),
            })
            .ToListAsync();
            foreach (var std in studys)
            {
                var rightMeeting = meetings.FirstOrDefault(d => d.Date == std.Date);
                if (rightMeeting != null)
                {
                    rightMeeting.StudyPerson = $"{std.MainFirst} {std.MainLast}";
                    rightMeeting.StudyReaderPerson = $"{std.VolunteerFirst} {std.VolunteerLast}";
                }
            }
            // var std = ctx.LmmAssignments.Where(d => d.LmmScheduleId)
            foreach (var item in myExc)
            {

                var ex = meetings.Where(d =>
                d.Date?.Week() == item.Date?.Week()
                && d.Date?.Year == item.Date2?.Year
                //&& d.Date?.Week() == item.Date2?.Week()
                ).ToList();
                foreach (var e in ex)
                {
                    e.IsException = true;
                    if (!string.IsNullOrEmpty(e.Chairman))
                    {
                        continue;
                    }
                    string typeName = "";
                    switch (item.Type)
                    {
                        case 1:
                            typeName = "Person?";
                            break;
                        case 2:
                            typeName = "Gedächtnismahl";
                            break;
                        case 4:
                            typeName = "Dienstwoche?";
                            break;
                        default:
                            typeName = $"Unbekannt {item.Type}";
                            break;
                    }
                    e.Chairman = $"{item.Desc} - {typeName}";
                }
            }
            /// Type 1 = Person?
            // Type 2 = Gedächtnismahl
            // Type 4 = Andere Ausnahme (Dienstwoche?) Publicmeetingday 7, Schoolday 5
            return meetings;
        }
    }

    public interface IChairStatisticsRepo
    {
        Task<List<ChairOverview>> GetChairOverview(DateTime from, DateTime to);
        Task<List<PartOverviewMeeting>> GetPartOverview(DateTime from, DateTime to, PartType partType);
        Task<List<AssignmentOverviewMeeting>> GetCompleteOverview(DateTime from, DateTime to, List<long?> personIds);
    }

}