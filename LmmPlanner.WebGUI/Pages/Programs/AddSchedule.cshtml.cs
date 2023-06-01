using System;
using System.Threading.Tasks;
using LmmPlanner.Data.TheocData;
using LmmPlanner.Entities.Interfaces;
using LmmPlanner.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LmmPlanner.WebGUI.Pages.Programs;
public class AddScheduleModel : PageModel
{

    public AddScheduleModel(IEditorRepo editorRepo)
    {
        _editorRepo = editorRepo;
        ActiveSchedule = new();
    }

    private readonly IEditorRepo _editorRepo;

    [BindProperty]
    public EditScheduleDto ActiveSchedule { get; set; }

    public void OnGet(DateTime meetingDate)
    {

        ActiveSchedule = new EditScheduleDto
        {
            Active = true,
            Date = meetingDate,
            StudyNumber = 0,
            Source = " "
        };
    }

    public async Task<IActionResult> OnPostAsync(long id)
    {
        TaskResult tr = await _editorRepo.SaveLmmSchedule(ActiveSchedule);
        if (tr.Success)
        {
            return RedirectToPage("/Schedule", new { date = ActiveSchedule.Date.ToString("yyyy-MM-dd") });
        }
        ModelState.AddModelError("", tr.Error);
        return Page();
    }
}