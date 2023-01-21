using System;
using System.Globalization;

namespace LmmPlanner.WebGUI.Helpers
{
public static class Extension {
    public static int Week(this DateTime date) {
        var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
        return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }
}
}