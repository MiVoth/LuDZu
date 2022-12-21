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
                    Chairman = s.ChairmanPerson == null ? "" : s.ChairmanPerson.Firstname + " " + s.ChairmanPerson.Lastname
                })
                .ToListAsync();
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
                    e.Chairman = $"{item.Desc} - ({item.Type})";
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
    }

}