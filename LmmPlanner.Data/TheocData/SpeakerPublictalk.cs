using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class SpeakerPublictalk
    {
        public long Id { get; set; }
        public long? SpeakerId { get; set; }
        public long? ThemeId { get; set; }
        public long? LangId { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
