using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class SchoolBreak
    {
        public long Id { get; set; }
        public byte[]? Date { get; set; }
        public long? Classnumber { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
