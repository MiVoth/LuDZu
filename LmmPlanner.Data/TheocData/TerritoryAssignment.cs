using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class TerritoryAssignment
    {
        public long Id { get; set; }
        public long? TerritoryId { get; set; }
        public long? PersonId { get; set; }
        public byte[]? CheckedoutDate { get; set; }
        public byte[]? CheckedbackinDate { get; set; }
        public long? LangId { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
