using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class LmmMeeting
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string? BibleReading { get; set; }
        public long? Chairman { get; set; }
        public long? Counselor2 { get; set; }
        public long? Counselor3 { get; set; }
        public long? PrayerBeginning { get; set; }
        public long? PrayerEnd { get; set; }
        public long? SongBeginning { get; set; }
        public long? SongMiddle { get; set; }
        public long? SongEnd { get; set; }
        public string? OpeningComments { get; set; }
        public string? ClosingComments { get; set; }
        public long? TimeStamp { get; set; }
        public bool? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
