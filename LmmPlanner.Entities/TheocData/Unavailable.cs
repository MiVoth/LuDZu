using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Unavailable
    {
        public long Id { get; set; }
        public long? PersonId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? TimeStamp { get; set; }
        public bool? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
