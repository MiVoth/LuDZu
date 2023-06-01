using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Data.TheocData;
using LmmPlanner.Entities.Models;

namespace LmmPlanner.Entities.Interfaces;

public interface IEditorRepo
{
    Task<long> NextAssignmentId();
    Task<bool> CommitChanges();
    void Remove(LmmSchedule schedule);
    Task<bool> UnavailabilityExists(long id);
    Task<Unavailable> GetUnavailability(long id);
    Task<TaskResult> SaveUnavailability(PersonNotAvailable unavailable);
    Task<TaskResult> UpdateUnavailability(PersonNotAvailable unavailable);
    Task<bool> DeleteUnavailability(long id);
    
    Task<LmmSchedule> GetLmmSchedule(long id);
    Task<EditScheduleDto> GetLmmScheduleDto(long id);
    Task<TaskResult> SaveLmmSchedule(EditScheduleDto dto);
    Task<TaskResult> UpdateLmmSchedule(EditScheduleDto dto);
    
    Task<LmmAssignment> GetLmmAssignment(long id);
    Task<List<LmmAssignment>> GetLmmScheduleAssignments(long scheduleId);
    Task<TaskResult> SaveLmmAssignment(long partId, long assigneeId);
}