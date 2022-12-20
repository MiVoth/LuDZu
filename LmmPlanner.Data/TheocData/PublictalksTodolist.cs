using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class PublictalksTodolist
    {
        public long Id { get; set; }
        public string Inout { get; set; } = null!;
        public string? Date { get; set; }
        public string? Speaker { get; set; }
        public string? Congregation { get; set; }
        public string? Theme { get; set; }
        public string? Notes { get; set; }
        public long? TimeStamp { get; set; }
        public string? Uuid { get; set; }
        public byte[]? Active { get; set; }
    }
}
