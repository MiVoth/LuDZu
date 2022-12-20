using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class ServiceMeeting
    {
        public long Id { get; set; }
        public byte[]? Date { get; set; }
        public byte[]? SongMiddle { get; set; }
        public byte[]? SongEnd { get; set; }
        public byte[]? PrayerId { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
