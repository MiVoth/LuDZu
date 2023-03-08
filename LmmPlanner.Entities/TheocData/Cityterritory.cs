using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Cityterritory
    {
        public string? City { get; set; }
        public long? Id { get; set; }
        public long? TerritoryNumber { get; set; }
        public string? Locality { get; set; }
        public long? CityId { get; set; }
        public long? TypeId { get; set; }
        public long? Priority { get; set; }
        public string? Remark { get; set; }
        public string? WktGeometry { get; set; }
        public long? LangId { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
