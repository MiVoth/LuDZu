using System;
using System.Threading.Tasks;
using LmmPlanner.Data.Statistics;
using LmmPlanner.Data.TheocData;
using LmmPlanner.Entities.Interfaces;
using LmmPlanner.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LmmPlanner.WebGUI.Pages.People;
public class DeleteUnavailableModel : PageModel
{
    private IEditorRepo _editorRepo;

    public DeleteUnavailableModel(IEditorRepo editorRepo)
    {
        _editorRepo = editorRepo;
        ActiveEntity = new();
    }

    [BindProperty]
    public PersonNotAvailable ActiveEntity { get; set; }

    public async Task OnGet(long id)
    {
        Unavailable unavailable = await _editorRepo.GetUnavailability(id);

        ActiveEntity = new()
        {
            Id = unavailable.Id,
            Active = unavailable.Active == true,
            From = unavailable.StartDate,
            To = unavailable.EndDate,
            PersonId = unavailable.PersonId
        };
    }

    public async Task<IActionResult> OnPost()
    {
        long id = ActiveEntity.PersonId;
        if (await _editorRepo.DeleteUnavailability(ActiveEntity.Id))
        {
            return RedirectToPage("./PublisherDetail", new { id });
        }
        return Page();
    }
}
