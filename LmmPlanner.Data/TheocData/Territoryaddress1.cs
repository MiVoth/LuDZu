using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Territoryaddress1
    {
        public long? Id { get; set; }
        public long? TerritoryId { get; set; }
        public long? TerritoryNumber { get; set; }
        public string? Locality { get; set; }
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
        public string? AddresstypeName { get; set; }
        public string? Color { get; set; }
        public long? LangId { get; set; }
    }
}
