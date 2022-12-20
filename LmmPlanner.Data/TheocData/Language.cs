using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Language
    {
        public long Id { get; set; }
        public string? Language1 { get; set; }
        public string? Desc { get; set; }
        public string? Code { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
