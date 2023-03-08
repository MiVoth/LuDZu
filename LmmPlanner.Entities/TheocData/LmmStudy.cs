using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class LmmStudy
    {
        public long Id { get; set; }
        public long? StudyNumber { get; set; }
        public string? Lang { get; set; }
        public string? StudyName { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
