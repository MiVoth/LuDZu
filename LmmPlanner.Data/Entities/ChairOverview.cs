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
    
}