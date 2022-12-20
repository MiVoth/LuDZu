using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Setting
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
