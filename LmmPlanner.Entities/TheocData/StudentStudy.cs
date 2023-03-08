using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class StudentStudy
    {
        public long Id { get; set; }
        public long? StudentId { get; set; }
        public long? StudyNumber { get; set; }
        public byte[]? StartDate { get; set; }
        public byte[]? EndDate { get; set; }
        public byte[]? Exercises { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
