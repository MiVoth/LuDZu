using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Data.TheocData;
using Microsoft.EntityFrameworkCore;

namespace LmmPlanner.Data.Statistics
{
    public interface IEditorRepo
    {
        Task<bool> CommitChanges();
        Task<LmmSchedule> GetLmmSchedule(long id);
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