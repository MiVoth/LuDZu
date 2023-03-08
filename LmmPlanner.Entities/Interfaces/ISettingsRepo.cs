using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Data.TheocData;
namespace LmmPlanner.Entities.Interfaces;

public interface ISettingsRepo
{
    Task<List<Setting>> GetSettings();
    Task<Setting?> GetSetting(long id);
    Task<Setting?> GetSetting(string name);
    Task<Congregation> GetCongregation(long id);
    Task<Congregation> GetCongregation();

    Task<bool> CommitChanges();
}
