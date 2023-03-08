using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Congregation
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Meeting1Time { get; set; }
        public string? Circuit { get; set; }
        public string? Info { get; set; }
        public long? TalkcoordinatorId { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
