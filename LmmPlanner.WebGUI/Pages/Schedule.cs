using System;
using System.Threading.Tasks;
using LmmPlanner.Entities.Interfaces;
using LmmPlanner.Entities.Models;
using LmmPlanner.WebGUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LmmPlanner.WebGUI.Pages;

public class ScheduleModel : BasePageModel
{
    private readonly ILogger<ScheduleModel> _logger;
    private readonly IFormFillerService _formFillerService;
    private readonly ISettingsRepo _settingsRepo;
    private readonly IScheduleRepo scheduleRepo;

    public ScheduleModel(ILogger<ScheduleModel> logger,
    ISettingsRepo settingsRepo,
    IFormFillerService formFillerService,
    IScheduleRepo scheduleRepo)
    {
        _logger = logger;
        _formFillerService = formFillerService;
        _settingsRepo = settingsRepo;
        this.scheduleRepo = scheduleRepo;
    }

    public MeetingInfo Meeting { get; set; } = new();
    public DateTime ActiveDate { get; set; } = DateTime.Now;
    public string HtmlExport { get; set; } = string.Empty;
    public async Task OnGet(DateTime? date)
    {
        // DateTime theDate = DateTime.Now;
        if (date != null)
        {
            ActiveDate = date.Value;
        }
        // LmmPlanner.Data


        Meeting = await scheduleRepo.GetSchedule(ActiveDate); //.GetAllPersons();
        // string html = await new Export.ExportService(_settingsRepo, scheduleRepo).Export();
        // HtmlExport = html;
    }

    public async Task OnPost(DateTime? date)
    {
        if (date != null)
        {
            ActiveDate = date.Value;
        }
        Meeting = await scheduleRepo.GetSchedule(ActiveDate); //.GetAllPersons();


    }

    public async Task<IActionResult> OnGetRefresh(DateTime date)
    {
        var meeting = await scheduleRepo.GetSchedule(date); //.GetAllPersons();
        return PartialView("_Schedule", meeting);
    }

    public async Task<IActionResult> OnGetSched(long partId, long? assignmentId = null, bool assist = false)
    {
        FittingPersons r = await scheduleRepo.GetPersonsToPart(partId, assist);
        r.AssignmentId = assignmentId;

        return PartialView("_PersonListing", r);
        // return new JsonResult(r);
    }

    public JsonResult OnGetExportS89(DateTime exportdate)
    {
        _formFillerService.Export(exportdate, exportdate.AddDays(7));
        return new JsonResult(new
        {

        });
        // FittingPersons r = await scheduleRepo.GetPersonsToPart(partId, assist);
        // r.AssignmentId = assignmentId;

        // return PartialView("_PersonListing", r);
        // return new JsonResult(r);
    }
}
