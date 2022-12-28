using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.Entities
{
    public class ChairOverview
    {
        public DateTime? Date { get; internal set; }
        public long? ChairmanId { get; internal set; }
        public string Chairman { get; internal set; } = string.Empty;
        public long LmmMeetingId { get; internal set; }
        public bool IsException { get; internal set; }
        public string StudyPerson { get; internal set; } = string.Empty;
        public string StudyReaderPerson { get; internal set; } = string.Empty;
        public string PrayerBeginning { get; internal set; } = string.Empty;
        public string PrayerEnd { get; internal set; } = string.Empty;
    }

    public class PartOverview
    {
        public List<PartOverviewMeeting> Meetings { get; set; }
        public List<LmmPerson> Persons { get; set; }

    }
    public class PartOverviewMeeting
    {
        public long LmmMeetingId { get; internal set; }
        public DateTime Date { get; internal set; }
        public PartOverviewSchedule Schedule { get; set; } = new();
        public long? PrayerBeginnung { get; internal set; }
        public long? Chairman { get; internal set; }
        public long? PrayerEnd { get; internal set; }
    }
    public class PartOverviewSchedule
    {
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public long? AssigneeId { get; internal set; }
    }

        public class AssignmentOverview
    {
        public List<AssignmentOverviewMeeting> Meetings { get; set; } = new();
        public List<LmmPerson> Persons { get; set; } = new();
    }
    public class AssignmentOverviewMeeting
    {
        public long LmmMeetingId { get; set; }
        public DateTime Date { get; set; }
        public long? Chairman { get; set; }
        public long? PrayerBeginning { get; set; }
        public long? PrayerEnd { get; set; }
        public List<AssignmentOverviewSchedule> Schedules { get; set; } = new();
    }
    public class AssignmentOverviewSchedule
    {
        public long? PersonId { get; set; }
        public DateTime? Date { get; set; }
        public string PartTypeName { get; set; } = string.Empty;
        public long? TalkId { get; internal set; }
    }
}