using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmmPlanner.Data.TheocData
{
    public partial class LmmSchedule
    {
        public long Id { get; set; }
        [Column(TypeName="date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public long? TalkId { get; set; }
        public string? Theme { get; set; }
        public string? Source { get; set; }
        public long? Time { get; set; }
        public long? TimeStamp { get; set; }
        public string? Uuid { get; set; }
        public bool? Active { get; set; }
        public long? StudyNumber { get; set; }
        public long? Roworder { get; set; }
    }

    public partial class LmmScheduleWrite
    {
        public long Id { get; set; }
        public string Date { get; set; } = null!;
        public long? TalkId { get; set; }
        public string? Theme { get; set; }
        public string? Source { get; set; }
        public long? Time { get; set; }
        public long? TimeStamp { get; set; }
        public string? Uuid { get; set; }
        public bool? Active { get; set; }
        public long? StudyNumber { get; set; }
        public long? Roworder { get; set; }
    }
}
