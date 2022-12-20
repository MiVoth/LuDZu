﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Data;
using LmmPlanner.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace LmmPlanner.WebGUI.Pages;

public class ScheduleModel : PageModel
{
    private readonly ILogger<ScheduleModel> _logger;
    private readonly ScheduleRepo scheduleRepo;

    public ScheduleModel(ILogger<ScheduleModel> logger, ScheduleRepo scheduleRepo)
    {
        _logger = logger;
        this.scheduleRepo = scheduleRepo;
    }

    public MeetingInfo Meeting { get; set; } = new();

    public async Task OnGet(DateTime? date)
    {
        DateTime theDate = DateTime.Now;
        if (date != null)
        {
            theDate = date.Value;
        }
        // LmmPlanner.Data

        Meeting = await scheduleRepo.GetSchedule(theDate); //.GetAllPersons();
    }

    public async Task<IActionResult> OnGetSched(long partId)
    {
        var r = await scheduleRepo.GetPersonsToPart(partId);

        var myViewData = new ViewDataDictionary(
            new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), 
            new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()) { { "_PersonListing", r.Persons } };
        myViewData.Model = r.Persons;

        PartialViewResult result = new PartialViewResult()
        {
            ViewName = "_PersonListing",
            ViewData = myViewData,
        };
        return result; 
        // return PartialView("_PersonListing", r.Persons);
        // return new JsonResult(r);
    }

    [NonAction]
    public virtual PartialViewResult PartialView(string viewName, object model)
    {
        ViewData.Model = model;

        return new PartialViewResult()
        {
            ViewName = viewName,
            ViewData = ViewData,
            TempData = TempData
        };
    }
}
