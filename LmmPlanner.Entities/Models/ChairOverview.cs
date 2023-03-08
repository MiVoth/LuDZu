using System;
using System.Collections.Generic;

namespace LmmPlanner.Entities.Models
{
    public class ChairOverview
    {
        public DateTime? Date { get; set; }
        public long? ChairmanId { get; set; }
        public string Chairman { get; set; } = string.Empty;
        public long LmmMeetingId { get; set; }
        public bool IsException { get; set; }
        public string StudyPerson { get; set; } = string.Empty;
        public string StudyReaderPerson { get; set; } = string.Empty;
        public string PrayerBeginning { get; set; } = string.Empty;
        public string PrayerEnd { get; set; } = string.Empty;
    }
    
}