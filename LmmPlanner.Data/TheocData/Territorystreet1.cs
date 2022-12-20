using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Territorystreet1
    {
        public long? Id { get; set; }
        public long? TerritoryId { get; set; }
        public long? TerritoryNumber { get; set; }
        public string? Locality { get; set; }
        public string? StreetName { get; set; }
        public string? FromNumber { get; set; }
        public string? ToNumber { get; set; }
        public long? Quantity { get; set; }
        public long? StreettypeId { get; set; }
        public string? StreettypeName { get; set; }
        public string? Color { get; set; }
        public string? WktGeometry { get; set; }
    }
}
