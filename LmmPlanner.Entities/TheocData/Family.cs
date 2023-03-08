using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Family
    {
        public long Id { get; set; }
        public long? PersonId { get; set; }
        public long? FamilyHead { get; set; }
        public long? TimeStamp { get; set; }
        public string? Uuid { get; set; }
    }
}
