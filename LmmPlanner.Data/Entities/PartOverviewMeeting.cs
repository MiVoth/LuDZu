using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.Entities
{
    public class PartOverviewMeeting
    {
        public long LmmMeetingId { get; internal set; }
        public DateTime Date { get; internal set; }
        public PartOverviewSchedule Schedule { get; set; } = new();
        public long? PrayerBeginnung { get; internal set; }
        public long? Chairman { get; internal set; }
        public long? PrayerEnd { get; internal set; }
    }

}