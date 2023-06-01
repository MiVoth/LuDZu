using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class LmmAssignment
    {
        public long Id { get; set; }
        public long? LmmScheduleId { get; set; }
        public long? Classnumber { get; set; }
        public long? AssigneeId { get; set; }
        public long? VolunteerId { get; set; }
        public long? AssistantId { get; set; }
        public DateTime? Date { get; set; }
        public bool? Completed { get; set; }
        public string? Note { get; set; }
        public string? Timing { get; set; }
        public string? Setting { get; set; }
        public long? TimeStamp { get; set; }
        public string? Uuid { get; set; }
    }

        public partial class LmmAssignmentWrite
    {
        public long Id { get; set; }
        public long? LmmScheduleId { get; set; }
        public long? Classnumber { get; set; }
        public long? AssigneeId { get; set; }
        public long? VolunteerId { get; set; }
        public long? AssistantId { get; set; }
        public string Date { get; set; } = null!;
        public bool? Completed { get; set; }
        public string? Note { get; set; }
        public string? Timing { get; set; }
        public string? Setting { get; set; }
        public long? TimeStamp { get; set; }
        public string? Uuid { get; set; }
    }
}
