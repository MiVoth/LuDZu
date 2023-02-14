using System;

namespace LmmPlanner.Data.Entities
{
    public class PartOverviewSchedule
    {
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public long? AssigneeId { get; internal set; }
        public long? AssigneeId2 { get; internal set; }
    }
}