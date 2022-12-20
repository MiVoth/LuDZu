using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class TerritoryAddresstype
    {
        public long Id { get; set; }
        public long? AddresstypeNumber { get; set; }
        public string? AddresstypeName { get; set; }
        public string? Color { get; set; }
        public long? LangId { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
