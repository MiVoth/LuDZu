using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Data.TheocData;
using LmmPlanner.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LmmPlanner.Data.Statistics
{
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
        public async Task<Setting?> GetSetting(string name)
        {
            return await ctx.Settings.FirstOrDefaultAsync(d => d.Name == name);
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

        public async Task<Congregation> GetCongregation()
        {
            string id = (await GetSetting("congregation_id"))?.Value ?? "1";
            return await GetCongregation(int.Parse(id));
        }
        public async Task<Congregation> GetCongregation(long id)
        {
            return await ctx.Congregations.FirstAsync(f => f.Id == id);
        }
    }

}