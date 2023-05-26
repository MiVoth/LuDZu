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

        public async Task OnGet(long personId, long? id)
        {
            if (id != null)
            {
                Unavailable unavailable = await _editorRepo.GetUnavailability(id.Value);

                ActiveEntity = new()
                {
                    Id = unavailable.Id,
                    Active = unavailable.Active == true,
                    From = unavailable.StartDate,
                    To = unavailable.EndDate,
                    PersonId = unavailable.PersonId ?? 0
                };
            }
            else
            {
                ActiveEntity = new()
                {
                    // Id = ,
                    Active = true,
                    From = DateTime.Today,
                    To = DateTime.Today,
                    PersonId = personId
                };
            }
        }

        public async Task<IActionResult> OnPost(long id)
        {
            Unavailable original = null!;
            if (await _editorRepo.UnavailabilityExists(id))
            {
                original = await _editorRepo.GetUnavailability(id);

            }
            else
            {
                original = new()
                {
                    PersonId = ActiveEntity.PersonId,
                };
                await _editorRepo.InsertUnavailability(original);
             }
            if (ActiveEntity == null)
            {
                throw new System.Exception("No!");
            }
            original.Active = ActiveEntity.Active;
            original.StartDate = ActiveEntity.From;
            original.EndDate = ActiveEntity.To;
            await _editorRepo.CommitChanges();
            return RedirectToPage("./PublisherDetails", new { id = original.PersonId });
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