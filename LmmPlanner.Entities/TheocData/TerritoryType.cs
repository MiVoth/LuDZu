using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class TerritoryType
    {
        public long Id { get; set; }
        public string? TypeName { get; set; }
        public long? LangId { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
