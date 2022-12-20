using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class ServiceProgram
    {
        public long Id { get; set; }
        public byte[]? MeetingId { get; set; }
        public byte[]? ProgramId { get; set; }
        public byte[]? PersonId { get; set; }
        public string? Theme { get; set; }
        public byte[]? Time { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
