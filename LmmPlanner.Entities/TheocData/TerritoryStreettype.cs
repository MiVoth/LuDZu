using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class TerritoryStreettype
    {
        public long Id { get; set; }
        public string? StreettypeName { get; set; }
        public string? Color { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
