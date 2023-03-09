using System;
using System.Threading.Tasks;
using LmmPlanner.Data.Statistics;
using LmmPlanner.Data.TheocData;
using LmmPlanner.Entities.Interfaces;
using LmmPlanner.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LmmPlanner.WebGUI.Pages.People
{
    public class EditUnavailableModel : PageModel
    {
        private IEditorRepo _editorRepo;

        public EditUnavailableModel(IEditorRepo editorRepo)
        {
            _editorRepo = editorRepo;
            ActiveEntity = new();
        }

        [BindProperty]
        public PersonNotAvailable ActiveEntity { get; set; }

        public async Task OnGet(long id)
        {
            Unavailable unavailable = await _editorRepo.GetUnavailability(id);
            ActiveEntity = new() {
                Id = unavailable.Id,
                Active = unavailable.Active == true,
                From = unavailable.StartDate,
                To = unavailable.EndDate,
                PersonId = unavailable.PersonId ?? 0
            };
        }

        public async Task<IActionResult> OnPost(long id)
        {
            Unavailable original = await _editorRepo.GetUnavailability(id);
            if (ActiveEntity == null)
            {
                throw new System.Exception("No!");
            }
            original.Active = ActiveEntity.Active;
            original.StartDate = ActiveEntity.From;
            original.EndDate = ActiveEntity.To;
            await _editorRepo.CommitChanges();
            return RedirectToPage("./Index", new { id = original.PersonId });
        }
        // public async Task<IActionResult> OnPostDelete(long id)
        // {
        //     var original = await _editorRepo.GetLmmSchedule(id);
        //     if (ActiveSchedule == null)
        //     {
        //         throw new System.Exception("No!");
        //     }
        //     _editorRepo.Remove(original);
        //     await _editorRepo.CommitChanges();
        //     return RedirectToPage("/Schedule", new { date = original.Date?.ToString("yyyy-MM-dd") });
        // }

    }
}