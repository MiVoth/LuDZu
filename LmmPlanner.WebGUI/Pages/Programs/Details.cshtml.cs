using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Data;
using LmmPlanner.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LmmPlanner.WebGUI.Pages.Programs;

public class DetailsModel : PageModel
{
    private readonly DataRepo dataRepo;
    private readonly ILogger<IndexModel> _logger;

    public DetailsModel(ILogger<IndexModel> logger,
        DataRepo dataRepo)
    {
        this.dataRepo = dataRepo;
        _logger = logger;
    }

    public LmmPerson ActivePerson { get; set; } = new();
    public List<LmmAssignmentInfo> Assignments { get; private set; } = new();

    public async Task OnGet(long personId)
    {
        // LmmPlanner.Data
        ActivePerson = await dataRepo.GetPerson(personId);
        Assignments = await dataRepo.GetAllAssignmentsOfPerson(personId);
    }
}
