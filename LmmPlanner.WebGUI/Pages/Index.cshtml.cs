using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Data;
using LmmPlanner.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LmmPlanner.WebGUI.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public List<LmmPerson> Persons { get; set; } = new();

    public void OnGet()
    {
    }
}
