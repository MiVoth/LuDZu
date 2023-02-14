using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.Entities
{
    public class AssignmentOverviewSchedule
    {
        public long? PersonId { get; set; }
        public long? AssistantId { get; set; }
        public DateTime? Date { get; set; }
        public string PartTypeName { get; set; } = string.Empty;
        public long? TalkId { get; internal set; }
    }
}