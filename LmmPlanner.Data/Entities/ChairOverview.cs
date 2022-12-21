using System;

namespace LmmPlanner.Data.Entities
{
    public class ChairOverview
    {
        public DateTime? Date { get; internal set; }
        public long? ChairmanId { get; internal set; }
        public string Chairman { get; internal set; } = string.Empty;
        public long LmmMeetingId { get; internal set; }
        public bool IsException { get; internal set; }
    }
}