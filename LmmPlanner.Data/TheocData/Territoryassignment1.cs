using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Territoryassignment1
    {
        public long? Id { get; set; }
        public long? TerritoryId { get; set; }
        public long? PersonId { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public byte[]? CheckedoutDate { get; set; }
        public byte[]? CheckedbackinDate { get; set; }
        public long? LangId { get; set; }
        public byte[]? Active { get; set; }
    }
}
