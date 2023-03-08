using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Entities.Models;

namespace LmmPlanner.Entities.Interfaces;

public interface IChairStatisticsRepo
{
    Task<List<ChairOverview>> GetChairOverview(DateTime from, DateTime to);
    Task<List<PartOverviewMeeting>> GetPartOverview(DateTime from, DateTime to, PartType partType);
    Task<List<AssignmentOverviewMeeting>> GetCompleteOverview(DateTime from, DateTime to, List<long?> personIds);
}
