using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class BiblestudyMeeting
    {
        public long Id { get; set; }
        public byte[]? Song { get; set; }
        public string? Material { get; set; }
        public byte[]? Date { get; set; }
        public byte[]? PersonId { get; set; }
        public byte[]? ReaderId { get; set; }
        public byte[]? PrayerId { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
