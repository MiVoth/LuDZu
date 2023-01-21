﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Data;
using LmmPlanner.Data.Entities;
using LmmPlanner.WebGUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace LmmPlanner.WebGUI.Pages;

public class ScheduleModel : BasePageModel
{
    private readonly ILogger<ScheduleModel> _logger;
    private readonly ScheduleRepo scheduleRepo;

    public ScheduleModel(ILogger<ScheduleModel> logger, ScheduleRepo scheduleRepo)
    {
        _logger = logger;
        this.scheduleRepo = scheduleRepo;
    }

    public MeetingInfo Meeting { get; set; } = new();
    public DateTime ActiveDate { get; set; } = DateTime.Now;
    public async Task OnGet(DateTime? date)
    {
        // DateTime theDate = DateTime.Now;
        if (date != null)
        {
            ActiveDate = date.Value;
        }
        // LmmPlanner.Data

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
        // var myViewData = new ViewDataDictionary(
        //     new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), 
        //     new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()) { { "_PersonListing", r.Persons } };
        // myViewData.Model = r.Persons;

        // PartialViewResult result = new PartialViewResult()
        // {
        //     ViewName = "_PersonListing",
        //     ViewData = myViewData,
        // };
        // return result; 
        return PartialView("_PersonListing", r);
        // return new JsonResult(r);
    }
}
