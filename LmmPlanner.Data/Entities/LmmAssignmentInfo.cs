namespace LmmPlanner.Data.Entities
{
    public class LmmAssignmentInfo
    {
        public long Id { get; internal set; }
        public string? MainName { get; internal set; }
        public string AssiName { get; internal set; } = string.Empty;
        public string VolName { get; internal set; } = string.Empty;
        public string? AssignDate { get; internal set; }
        public string? Theme { get; internal set; }
        public string? Source { get; internal set; }
        public long? StudyNumber { get; internal set; }
        public long? TalkId { get; internal set; }
    }
}