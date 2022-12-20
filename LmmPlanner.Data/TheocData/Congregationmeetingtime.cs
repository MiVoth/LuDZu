using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Congregationmeetingtime
    {
        public long Id { get; set; }
        public long CongregationId { get; set; }
        public long MtgYear { get; set; }
        public long MtgDay { get; set; }
        public string MtgTime { get; set; } = null!;
        public long? TimeStamp { get; set; }
        public string? Uuid { get; set; }
    }
}
