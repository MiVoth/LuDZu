﻿using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class SchoolSchedule
    {
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public long? Number { get; set; }
        public string? Theme { get; set; }
        public string? Source { get; set; }
        public bool? Onlybrothers { get; set; }
        public long? TimeStamp { get; set; }
        public bool? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
