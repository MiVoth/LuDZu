using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LmmPlanner.Entities.Models;
public class TaskResult
{
    public TaskResult()
    {

    }
    public TaskResult(string error)
    {
        Errors.Add(new(error));
    }
    public bool Success { get; set; }
    public List<ValidationResult> Errors { get; set; } = new();
    public string Error { get => string.Join(",", Errors.Select(e => e.ErrorMessage)); }
}