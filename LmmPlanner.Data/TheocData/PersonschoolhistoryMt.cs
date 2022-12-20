using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class PersonschoolhistoryMt
    {
        public byte[]? Active { get; set; }
        public long? Id { get; set; }
        public long? StudentId { get; set; }
        public long? AssistantId { get; set; }
        public long? CongregationId { get; set; }
        public string? Gender { get; set; }
        public long? Usefor { get; set; }
        public byte[]? Servant { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public long? VolunteerId { get; set; }
        public byte[]? Date { get; set; }
        public long? Number { get; set; }
        public long? Classnumber { get; set; }
    }
}
