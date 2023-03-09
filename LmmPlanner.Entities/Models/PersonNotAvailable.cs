using System;

namespace LmmPlanner.Entities.Models;
public class PersonNotAvailable
{
    public long Id { get; set; }
    public long PersonId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public bool Active { get; set; }
}