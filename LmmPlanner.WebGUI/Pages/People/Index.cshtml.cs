using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Data;
using LmmPlanner.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LmmPlanner.WebGUI.Pages.People;

public class IndexModel : PageModel
{
    private readonly DataRepo dataRepo;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, DataRepo dataRepo)
    {
        this.dataRepo = dataRepo;
        _logger = logger;
    }

    public List<LmmPerson> Persons { get; set; } = new();

    public async Task OnGet()
    {
        Persons = await dataRepo.GetAllPersons();
    }
}
