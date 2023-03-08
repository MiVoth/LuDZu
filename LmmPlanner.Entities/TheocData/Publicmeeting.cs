using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Publicmeeting
    {
        public long Id { get; set; }
        public byte[]? Date { get; set; }
        public long? ChairmanId { get; set; }
        public long? ThemeId { get; set; }
        public long? SpeakerId { get; set; }
        public string? FinalTalk { get; set; }
        public long? HospitalityId { get; set; }
        public string? WtSource { get; set; }
        public string? WtTheme { get; set; }
        public long? WtConductorId { get; set; }
        public long? WtreaderId { get; set; }
        public long? SongPt { get; set; }
        public long? SongWtStart { get; set; }
        public long? SongWtEnd { get; set; }
        public long? OpeningPrayerId { get; set; }
        public long? FinalPrayerId { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
