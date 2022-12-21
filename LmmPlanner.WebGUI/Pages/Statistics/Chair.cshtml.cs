using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Data.Entities;
using LmmPlanner.Data.Statistics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LmmPlanner.WebGUI.Pages.Statistics
{
    public class ChairModel : PageModel
    {
        private IChairStatisticsRepo _chairRepo;

        public ChairModel(IChairStatisticsRepo chairRepo)
        {
            _chairRepo = chairRepo;
            ChairOverviews = new();
        }
        public List<ChairOverview> ChairOverviews { get; set; }
        public async Task OnGet()
        {
            ChairOverviews = await _chairRepo.GetChairOverview(DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1));
        }
    }
}