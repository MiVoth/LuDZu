using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Publicmeetinghistory
    {
        public byte[]? Weekof { get; set; }
        public byte[]? MtgDate { get; set; }
        public long? ThemeId { get; set; }
        public long? ThemeNumber { get; set; }
        public string? Theme { get; set; }
        public long? SpeakerId { get; set; }
        public long? ChairmanId { get; set; }
        public long? WtreaderId { get; set; }
        public long? WtConductorId { get; set; }
        public string? WtSource { get; set; }
        public string? WtTheme { get; set; }
        public string? FinalTalk { get; set; }
        public long? SongPt { get; set; }
        public long? SongWtStart { get; set; }
        public long? SongWtEnd { get; set; }
        public long? HospitalityId { get; set; }
        public long? FinalPrayerId { get; set; }
        public long? Id { get; set; }
    }
}
