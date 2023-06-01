using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LmmPlanner.Data.TheocData;
using LmmPlanner.Entities.Interfaces;
using LmmPlanner.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LmmPlanner.Data.Statistics
{
    public class EditorRepo : IEditorRepo
    {
        private readonly MyWriterContext _writerContext;
        private readonly IMapper _mapper;
        private MyContext ctx;

        public EditorRepo(MyContext context,
        MyWriterContext writerContext,
        IMapper mapper)
        {
            _writerContext = writerContext;
            _mapper = mapper;
            ctx = context;
        }
        // public async Task<List<Setting>> GetSettings()
        // {
        //     return await ctx.Settings.ToListAsync();
        // }

        public async Task<TaskResult> SaveLmmSchedule(EditScheduleDto dto)
        {
            LmmScheduleWrite newSchedule = _mapper.Map<LmmScheduleWrite>(dto);

            newSchedule.Id = 1 + _writerContext.LmmSchedules.Max(f => f.Id);
            newSchedule.Date = dto.Date.ToString("yyyy-MM-dd");
            newSchedule.Uuid = System.Guid.NewGuid().ToString();
            newSchedule.TimeStamp = System.DateTimeOffset.Now.ToUnixTimeSeconds();
            _writerContext.Add(newSchedule);
            return await _writerContext.SaveChangesAsync();
        }

        public async Task<TaskResult> UpdateLmmSchedule(EditScheduleDto dto)
        {
            if (dto.Id == null)
            {
                return new("Nicht gefunden") { };
            }
            LmmScheduleWrite dbSched = _writerContext.LmmSchedules.First(d => d.Id == dto.Id.Value);

            dbSched = _mapper.Map(dto, dbSched);
            // LmmScheduleWrite newSchedule = _mapper.Map<LmmScheduleWrite>(dto);

            // newSchedule.Id = 1 + _writerContext.LmmSchedules.Max(f => f.Id);
            dbSched.Date = dto.Date.ToString("yyyy-MM-dd");
            // newSchedule.Uuid = System.Guid.NewGuid().ToString();
            // newSchedule.TimeStamp = System.DateTimeOffset.Now.ToUnixTimeSeconds();
            // _writerContext.Add(newSchedule);
            return await _writerContext.SaveChangesAsync();
        }
        public async Task<EditScheduleDto> GetLmmScheduleDto(long id)
        {
            LmmSchedule sched = await GetLmmSchedule(id);
            return _mapper.Map<EditScheduleDto>(sched);
        }
        public async Task<LmmSchedule> GetLmmSchedule(long id)
        {
            return await ctx.LmmSchedules.FirstAsync(d => d.Id == id);
        }
        public void Remove(LmmSchedule schedule)
        {
            ctx.Remove(schedule);
        }
        public async Task<List<LmmAssignment>> GetLmmScheduleAssignments(long scheduleId)
        {
            return await ctx.LmmAssignments.Where(d => d.LmmScheduleId == scheduleId).ToListAsync();
        }
        public async Task<LmmAssignment> GetLmmAssignment(long id)
        {
            return await ctx.LmmAssignments.FirstAsync(d => d.Id == id);
        }


        public async Task<TaskResult> SaveLmmAssignment(long partId, long assigneeId)
        {
            var original = await GetLmmSchedule(partId);
            var assignments = await GetLmmScheduleAssignments(partId);
            if (assignments.Count == 0)
            {
                var assignment = new LmmAssignmentWrite
                {
                    Id = await NextAssignmentId(),
                    AssigneeId = assigneeId,
                    Classnumber = 1,
                    Date = original.Date.ToString("yyyy-MM-dd"), //?.AddDays(4),
                    LmmScheduleId = partId,
                    TimeStamp = System.DateTimeOffset.Now.ToUnixTimeSeconds(),
                    Uuid = $"{System.Guid.NewGuid()}"
                };
                _writerContext.Add(assignment);
            }
            return await _writerContext.SaveChangesAsync();
        }
        public async Task<bool> CommitChanges()
        {
            try
            {
                await ctx.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<long> NextAssignmentId()
        {
            long id = await ctx.Set<LmmAssignment>().MaxAsync(d => d.Id);
            return id + 1;
        }

        public async Task<Unavailable> GetUnavailability(long id)
        {
            return await ctx.Unavailables.FirstAsync(d => d.Id == id);
        }

        public async Task<bool> UnavailabilityExists(long id)
        {
            return await ctx.Unavailables.AnyAsync(d => d.Id == id);
        }
        public async Task<TaskResult> UpdateUnavailability(PersonNotAvailable unavailable)
        {
            UnavailableWrite un = _writerContext.Unavailables.First(f => f.Id == unavailable.Id);
            un.StartDate = $"{unavailable.From?.ToString("yyyy-MM-dd")}";
            un.EndDate = $"{unavailable.To?.ToString("yyyy-MM-dd")}";
            un.TimeStamp = System.DateTimeOffset.Now.ToUnixTimeSeconds();
            if (un.Uuid == null)
            {
                un.Uuid = System.Guid.NewGuid().ToString();
            }
            // _writerContext.Add(un);
            // await ctx.AddAsync(unavailable);
            return await _writerContext.SaveChangesAsync();
        }

        public async Task<TaskResult> SaveUnavailability(PersonNotAvailable unavailable)
        {
            UnavailableWrite un = new()
            {
                Id = await NextAssignmentId(),
                Active = true,
                PersonId = unavailable.PersonId,
                StartDate = $"{unavailable.From?.ToString("yyyy-MM-dd")}",
                EndDate = $"{unavailable.To?.ToString("yyyy-MM-dd")}",
                Uuid = System.Guid.NewGuid().ToString(),
                TimeStamp = System.DateTimeOffset.Now.ToUnixTimeSeconds()
            };
            _writerContext.Add(un);
            // await ctx.AddAsync(unavailable);
            return await _writerContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteUnavailability(long id)
        {
            Unavailable un = await ctx.Unavailables.FirstAsync(f => f.Id == id);
            ctx.Remove(un);
            try
            {
                await ctx.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}