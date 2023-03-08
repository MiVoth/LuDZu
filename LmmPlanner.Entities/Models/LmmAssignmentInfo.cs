namespace LmmPlanner.Entities.Models
{
    public class LmmAssignmentInfo
    {
        public long Id { get; set; }
        public string? MainName { get; set; }
        public string AssiName { get; set; } = string.Empty;
        public string VolName { get; set; } = string.Empty;
        public string? AssignDate { get; set; }
        public string? Theme { get; set; }
        public string? Source { get; set; }
        public long? StudyNumber { get; set; }
        public long? TalkId { get; set; }
    }
}