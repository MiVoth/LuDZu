using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class School
    {
        public long Id { get; set; }
        public long? SchoolScheduleId { get; set; }
        public long? Classnumber { get; set; }
        public long? StudentId { get; set; }
        public long? VolunteerId { get; set; }
        public long? AssistantId { get; set; }
        public byte[]? Date { get; set; }
        public byte[]? Completed { get; set; }
        public string? Note { get; set; }
        public string? Timing { get; set; }
        public long? SettingId { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
