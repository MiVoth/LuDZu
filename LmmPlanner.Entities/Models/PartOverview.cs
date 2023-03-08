using System.Collections.Generic;

namespace LmmPlanner.Entities.Models
{
    public class PartOverview
    {
        public List<PartOverviewMeeting> Meetings { get; set; } = new();
        public List<LmmPerson> Persons { get; set; } = new();

    }
}