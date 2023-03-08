using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class TerritoryStreet
    {
        public long Id { get; set; }
        public long? TerritoryId { get; set; }
        public string? StreetName { get; set; }
        public string? FromNumber { get; set; }
        public string? ToNumber { get; set; }
        public long? Quantity { get; set; }
        public long? StreettypeId { get; set; }
        public string? WktGeometry { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
