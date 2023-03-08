using System;

namespace LmmPlanner.Entities.Models
{
    public class PartOverviewSchedule
    {
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public long? AssigneeId { get; set; }
        public long? AssigneeId2 { get; set; }
    }
}