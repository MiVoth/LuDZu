﻿using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Exception
    {
        public long Id { get; set; }
        public long? Type { get; set; }
        public byte[]? Date { get; set; }
        public byte[]? Date2 { get; set; }
        public long? Schoolday { get; set; }
        public long? Publicmeetingday { get; set; }
        public string? Desc { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
        public long? Cbsday { get; set; }
    }
}
