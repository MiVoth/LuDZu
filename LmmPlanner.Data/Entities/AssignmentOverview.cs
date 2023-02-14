using System.Collections.Generic;

namespace LmmPlanner.Data.Entities
{
    public class AssignmentOverview
    {
        public List<AssignmentOverviewMeeting> Meetings { get; set; } = new();
        public List<LmmPerson> Persons { get; set; } = new();
    }
}