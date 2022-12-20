using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class TerritoryCity
    {
        public long Id { get; set; }
        public string? City { get; set; }
        public long? LangId { get; set; }
        public string? WktGeometry { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
