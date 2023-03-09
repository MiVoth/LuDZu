using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Data;
using LmmPlanner.Entities.Interfaces;
using LmmPlanner.Entities.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LmmPlanner.WebGUI.Pages.People;

public class PublisherDetailsModel : PageModel
{
    private readonly IDataRepo dataRepo;
    private readonly ILogger<IndexModel> _logger;

    public PublisherDetailsModel(ILogger<IndexModel> logger,
        IDataRepo dataRepo)
    {
        this.dataRepo = dataRepo;
        _logger = logger;
    }

    public LmmPerson ActivePerson { get; set; } = new();
    public List<LmmAssignmentInfo> Assignments { get; private set; } = new();
    public List<PersonNotAvailable> PersonExceptions { get; private set; } = new();

    public async Task OnGet(long personId)
    {
        // LmmPlanner.Data
        ActivePerson = await dataRepo.GetPerson(personId);
        Assignments = await dataRepo.GetAllAssignmentsOfPerson(personId);
        PersonExceptions = await dataRepo.GetPersonNotAvailableAsync(personId);
    }
}
