using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class SchoolSetting
    {
        public long Id { get; set; }
        public long? Number { get; set; }
        public long? Name { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
        public byte[]? Brothers { get; set; }
    }
}
