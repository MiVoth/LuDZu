using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Song
    {
        public long Id { get; set; }
        public long? SongNumber { get; set; }
        public string? Title { get; set; }
        public long? LangId { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
