using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Data.TheocData;

namespace LmmPlanner.Entities.Interfaces;

public interface IEditorRepo
{
    Task<long> NextAssignmentId();
    Task<bool> CommitChanges();
    void Remove(LmmSchedule schedule);
    Task<bool> UnavailabilityExists(long id);
    Task<Unavailable> GetUnavailability(long id);
    Task<bool> InsertUnavailability(Unavailable unavailable);
    Task<bool> DeleteUnavailability(long id);
    Task<LmmSchedule> GetLmmSchedule(long id);
    Task<LmmAssignment> GetLmmAssignment(long id);
    Task<List<LmmAssignment>> GetLmmScheduleAssignments(long scheduleId);
    Task<LmmAssignment> AddAssignment(LmmAssignment assign);
}