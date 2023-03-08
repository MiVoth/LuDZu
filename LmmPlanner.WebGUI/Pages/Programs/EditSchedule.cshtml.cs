using System;
using System.Threading.Tasks;
using LmmPlanner.Data.Statistics;
using LmmPlanner.Data.TheocData;
using LmmPlanner.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LmmPlanner.WebGUI.Pages.Programs
{
    public class EditScheduleModel : PageModel
    {
        private IEditorRepo _editorRepo;

        public EditScheduleModel(IEditorRepo editorRepo)
        {
            _editorRepo = editorRepo;
            ActiveSchedule = new();
        }

        [BindProperty]
        public LmmSchedule ActiveSchedule { get; set; }

        public async Task OnGet(long id)
        {
            ActiveSchedule = await _editorRepo.GetLmmSchedule(id);
        }

        public async Task<IActionResult> OnPost(long id)
        {
            var original = await _editorRepo.GetLmmSchedule(id);
            if (ActiveSchedule == null)
            {
                throw new System.Exception("No!");
            }
            original.Theme = ActiveSchedule.Theme;
            original.Time = ActiveSchedule.Time;
            await _editorRepo.CommitChanges();
            return RedirectToPage("/Schedule", new { date = original.Date?.ToString("yyyy-MM-dd") });
        }
        public async Task<IActionResult> OnPostDelete(long id)
        {
            var original = await _editorRepo.GetLmmSchedule(id);
            if (ActiveSchedule == null)
            {
                throw new System.Exception("No!");
            }
            _editorRepo.Remove(original);
            await _editorRepo.CommitChanges();
            return RedirectToPage("/Schedule", new { date = original.Date?.ToString("yyyy-MM-dd") });
        }

        public async Task<JsonResult> OnPostAssignee(long partId, long? assignmentId, long assigneeId, bool assist)
        {
            if (assignmentId == null || assignmentId == 0)
            {
                var original = await _editorRepo.GetLmmSchedule(partId);
                var assignments = await _editorRepo.GetLmmScheduleAssignments(partId);
                if (assignments.Count == 0)
                {
                    var assignment = new LmmAssignment
                    {
                        Id = await _editorRepo.NextAssignmentId(),
                        AssigneeId = assigneeId,
                        Classnumber = 1,
                        Date = original.Date?.AddDays(4),
                        LmmScheduleId = partId,
                        TimeStamp = DateTime.Now.ToFileTimeUtc(),
                        Uuid = $"{Guid.NewGuid()}"
                    };
                    await _editorRepo.AddAssignment(assignment);
                }
            }
            else
            {
                var original = await _editorRepo.GetLmmAssignment(assignmentId.Value);
                if (!assist)
                {
                    original.AssigneeId = assigneeId;
                }
                else
                {
                    // original.VolunteerId = assigneeId;
                    original.AssistantId = assigneeId;
                }
            }
            bool success = await _editorRepo.CommitChanges();
            return new JsonResult(new
            {
                Success = success
            });
        }
    }
}