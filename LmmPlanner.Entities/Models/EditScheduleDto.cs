using System;
namespace LmmPlanner.Entities.Models;
public class EditScheduleDto
{
    public long? Id { get; set; }
    public DateTime Date { get; set; }
    public long? TalkId { get; set; }
    public string? Theme { get; set; }
    public string? Source { get; set; }
    public long? Time { get; set; }
    public bool? Active { get; set; }
    public long? StudyNumber { get; set; }
    public long? Roworder { get; set; }
}