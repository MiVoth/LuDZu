using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Note
    {
        public long Id { get; set; }
        public long? TypeId { get; set; }
        public byte[]? Date { get; set; }
        public string? Notes { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
