using System.Threading.Tasks;
using LmmPlanner.Data.Statistics;
using LmmPlanner.Data.TheocData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LmmPlanner.WebGUI.Pages.Config
{
    public class SettingEditModel : PageModel
    {
        private ISettingsRepo _settingsRepo;
        public SettingEditModel(ISettingsRepo settingsRepo)
        {
            _settingsRepo = settingsRepo;
            ActiveSetting = new();
        }

        [BindProperty]
        public Setting ActiveSetting { get; set; }

        public async Task OnGet(long id)
        {
            ActiveSetting = await _settingsRepo.GetSetting(id) ?? new();
        }

        public async Task<IActionResult> OnPost(long id)
        {
            var oldSetting = await _settingsRepo.GetSetting(id);
            if (oldSetting == null)
            {
                throw new System.Exception("No");
            }
            oldSetting.Name = ActiveSetting.Name;
            oldSetting.Value = ActiveSetting.Value;
            await _settingsRepo.CommitChanges();
            return RedirectToPage("./Settings");
        }
    }
}