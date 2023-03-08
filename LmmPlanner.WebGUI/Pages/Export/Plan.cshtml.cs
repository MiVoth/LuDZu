using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LmmPlanner.Data;
using LmmPlanner.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LmmPlanner.WebGUI.Pages.Export;

public class PlanModel : PageModel
{
    private readonly IExportService _exportService;
    private readonly ISettingsRepo _settingsRepo;
    private readonly ILogger<IndexModel> _logger;

    public PlanModel(ILogger<IndexModel> logger,
    IExportService exportService,
        ISettingsRepo settingsRepo)
    {
        _exportService = exportService;
        _settingsRepo = settingsRepo;
        _logger = logger;
        StartDate = DateTime.Today;
        EndDate = DateTime.Today;
    }
    [BindProperty]
    public DateTime StartDate { get; set; }
    [BindProperty]
    public DateTime EndDate { get; set; }

    public void OnGet()
    {

    }
    public async Task<FileResult> OnPost()
    {
        var ups = await _exportService.ExportToPdfAsync(StartDate, EndDate);


        return File(ups, "application/pdf", "download.pdf");
    }

    public async Task<IActionResult> OnGetExportPlan(DateTime start, DateTime end)
    {
        string html = await _exportService.ExportAsync(start, end);
        return Content(html, "text/html"); // new ActionResult<string>(html);
        // return html;
        // string html = await new Export.ExportService(_settingsRepo, scheduleRepo).Export();
        // HtmlExport = html;
    }
    public async Task<JsonResult> OnGetExportPlan2(DateTime start, DateTime end)
    {
        string html = await _exportService.ExportAsync(start, end);
        // string html = await new Export.ExportService(_settingsRepo, scheduleRepo).Export();
        // HtmlExport = html;
        return new JsonResult(new
        {
            Success = true,
            Result = html
        });
    }
}
