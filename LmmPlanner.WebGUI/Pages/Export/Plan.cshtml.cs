using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LmmPlanner.WebGUI.Pages.Export;

public class PlanModel : PageModel
{
    private readonly DataRepo dataRepo;
    private readonly ILogger<IndexModel> _logger;

    public PlanModel(ILogger<IndexModel> logger,
        DataRepo dataRepo)
    {
        this.dataRepo = dataRepo;
        _logger = logger;
        StartDate = DateTime.Now;
        EndDate = DateTime.Now;
    }


    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public async Task OnGet(long personId)
    {

    }

    public async Task<JsonResult> OnGetExportPlan(DateTime start, DateTime end)
    {
        // string html = await new Export.ExportService(_settingsRepo, scheduleRepo).Export();
        // HtmlExport = html;
        return new JsonResult(new {
            Success = true
        });
    }
}
