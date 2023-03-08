using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LmmPlanner.Entities.Interfaces;
using LmmPlanner.Entities.Models;
using LmmPlanner.WebGUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace LmmPlanner.WebGUI.Pages.Statistics
{
    public class ChairModel : BasePageModel
    {
        private IDataRepo _dataRepo;
        private IChairStatisticsRepo _chairRepo;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public ChairModel(IChairStatisticsRepo chairRepo,
        IDataRepo dataRepo
        )
        {
            int weeks = 10;
            FromDate = DateTime.Now.AddDays(2 * 7 * -1);
            ToDate = DateTime.Now.AddDays(weeks * 7);
            _dataRepo = dataRepo;
            _chairRepo = chairRepo;
            ChairOverviews = new();
        }
        public List<ChairOverview> ChairOverviews { get; set; }
        public async Task OnGet()
        {
            ChairOverviews = await _chairRepo.GetChairOverview(DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1));
        }

        public async Task<IActionResult> OnGetPart(PartType partType, DateTime fromDate, DateTime toDate)
        {

            var part1 = await _chairRepo.GetPartOverview(fromDate, toDate, partType);
            var persons = await _dataRepo.GetAllPersons();
            persons = persons.Where(p => p.MayTakePart(partType))
                .OrderBy(p => p.Lastname).ThenBy(p => p.Firstname).ToList();
            var moel = new PartOverview
            {
                Meetings = part1.OrderBy(d => d.Date).ToList(),
                Persons = persons
            };
            return PartialView("_Monthly", moel);
            // var myViewData = new ViewDataDictionary(
            //     new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),
            //     new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()) { { "_Monthly", moel } };
            // myViewData.Model = moel;
            // PartialViewResult result = new PartialViewResult()
            // {
            //     ViewName = "_Monthly",
            //     ViewData = myViewData,
            // };
            // return result;
        }

        public async Task<IActionResult> OnGetAll(DateTime fromDate, DateTime toDate)
        {

            var persons = await _dataRepo.GetAllPersons();
            persons = persons.Where(p => p.IsServant)
                .OrderBy(p => p.Lastname).ThenBy(p => p.Firstname).ToList();
            var part1 = await _chairRepo.GetCompleteOverview(fromDate, toDate, persons.Select(d => (long?)d.Id).ToList());
            var moel = new AssignmentOverview
            {
                Meetings = part1.OrderBy(d => d.Date).ToList(),
                Persons = persons
            };
            return PartialView("_Complete", moel);
        }
    }
}