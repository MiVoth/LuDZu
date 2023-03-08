using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Person
    {
        public long Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public bool Servant { get; set; }
        public bool Elder { get; set; }
        public long? Publisher { get; set; }
        public string? Gender { get; set; }
        public long? Usefor { get; set; }
        public long? CongregationId { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Info { get; set; }
        public long? TimeStamp { get; set; }
        // public byte[]? Active { get; set; }
        public bool? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
