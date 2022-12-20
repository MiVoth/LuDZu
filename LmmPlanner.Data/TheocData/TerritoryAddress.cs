using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class TerritoryAddress
    {
        public long Id { get; set; }
        public long? TerritoryId { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? County { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? Housenumber { get; set; }
        public string? Postalcode { get; set; }
        public string? WktGeometry { get; set; }
        public string? Name { get; set; }
        public long? AddresstypeNumber { get; set; }
        public long? LangId { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
