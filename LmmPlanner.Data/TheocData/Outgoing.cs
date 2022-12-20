using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Outgoing
    {
        public long Id { get; set; }
        public byte[]? Date { get; set; }
        public byte[]? SpeakerId { get; set; }
        public byte[]? CongregationId { get; set; }
        public byte[]? ThemeId { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
