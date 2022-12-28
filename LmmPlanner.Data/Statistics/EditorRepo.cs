using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LmmPlanner.Data.TheocData;
using Microsoft.EntityFrameworkCore;

namespace LmmPlanner.Data.Statistics
{
    public interface IEditorRepo
    {
        Task<bool> CommitChanges();
        Task<LmmSchedule> GetLmmSchedule(long id);
        Task<LmmAssignment> GetLmmAssignment(long id);
        Task<List<LmmAssignment>> GetLmmScheduleAssignments(long scheduleId);
    }

    public class EditorRepo : IEditorRepo
    {
        private MyContext ctx;

        public EditorRepo(MyContext context)
        {
            ctx = context;
        }

        // public async Task<List<Setting>> GetSettings()
        // {
        //     return await ctx.Settings.ToListAsync();
        // }
        public async Task<LmmSchedule> GetLmmSchedule(long id)
        {
            return await ctx.LmmSchedules.FirstAsync(d => d.Id == id);
        }
        public async Task<List<LmmAssignment>> GetLmmScheduleAssignments(long scheduleId)
        {
            return await ctx.LmmAssignments.Where(d => d.LmmScheduleId == scheduleId).ToListAsync();
        }
        public async Task<LmmAssignment> GetLmmAssignment(long id)
        {
            return await ctx.LmmAssignments.FirstAsync(d => d.Id == id);
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
    }

}