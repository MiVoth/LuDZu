using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class EReminder
    {
        public long Id { get; set; }
        public byte[]? Type { get; set; }
        public byte[]? ObjectId { get; set; }
        public byte[]? UserId { get; set; }
        public byte[]? Counter { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
