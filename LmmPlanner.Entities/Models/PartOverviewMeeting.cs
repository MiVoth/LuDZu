using System;
using System.Collections.Generic;

namespace LmmPlanner.Entities.Models
{
    public class PartOverviewMeeting
    {
        public long LmmMeetingId { get; set; }
        public DateTime Date { get; set; }
        public PartOverviewSchedule Schedule { get; set; } = new();
        public long? PrayerBeginnung { get; set; }
        public long? Chairman { get; set; }
        public long? PrayerEnd { get; set; }
    }

}