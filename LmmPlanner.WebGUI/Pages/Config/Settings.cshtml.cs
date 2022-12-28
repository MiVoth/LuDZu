using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Data.Statistics;
using LmmPlanner.Data.TheocData;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LmmPlanner.WebGUI.Pages.Config
{
    public class SettingsModel : PageModel
    {
        private ISettingsRepo _settingsRepo;

        public SettingsModel(ISettingsRepo settingsRepo)
        {
            _settingsRepo = settingsRepo;
            AllSettings = new();
        }

        public List<Setting> AllSettings { get; private set; }

        public async Task OnGet()
        {
            AllSettings = await _settingsRepo.GetSettings();
        }
    }
}