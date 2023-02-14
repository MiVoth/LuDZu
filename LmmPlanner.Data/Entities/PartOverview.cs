using System.Collections.Generic;

namespace LmmPlanner.Data.Entities
{
    public class PartOverview
    {
        public List<PartOverviewMeeting> Meetings { get; set; } = new();
        public List<LmmPerson> Persons { get; set; } = new();

    }
}