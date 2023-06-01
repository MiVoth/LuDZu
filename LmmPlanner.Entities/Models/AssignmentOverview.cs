using System.Collections.Generic;

namespace LmmPlanner.Entities.Models
{
    public class AssignmentOverview
    {
        public List<AssignmentOverviewMeeting> Meetings { get; set; } = new();
        public List<LmmPersonExtented> Persons { get; set; } = new();
    }
}