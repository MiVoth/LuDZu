using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Data.TheocData;
using Microsoft.EntityFrameworkCore;

namespace LmmPlanner.Data.Statistics
{
    public interface ISettingsRepo
    {
        Task<List<Setting>> GetSettings();
        Task<Setting?> GetSetting(long id);

        Task<bool> CommitChanges();
    }

    public class SettingsRepo : ISettingsRepo
    {
        private MyContext ctx;

        public SettingsRepo(MyContext context)
        {
            ctx = context;
        }

        public async Task<List<Setting>> GetSettings()
        {
            return await ctx.Settings.ToListAsync();
        }
        public async Task<Setting?> GetSetting(long id)
        {
            return await ctx.Settings.FirstOrDefaultAsync(d => d.Id == id);
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