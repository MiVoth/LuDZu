using System;
using System.Collections.Generic;

namespace LmmPlanner.Entities.Models
{
    public class AssignmentOverviewMeeting
    {
        public long LmmMeetingId { get; set; }
        public DateTime Date { get; set; }
        public long? Chairman { get; set; }
        public long? PrayerBeginning { get; set; }
        public long? PrayerEnd { get; set; }
        public List<AssignmentOverviewSchedule> Schedules { get; set; } = new();
    }
}