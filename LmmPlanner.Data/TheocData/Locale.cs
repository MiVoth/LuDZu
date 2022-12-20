using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Locale
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public long? LanguageId { get; set; }
        public string? SamemonthRangeformat { get; set; }
        public string? SameyearRangeformat { get; set; }
        public string? GeneralRangeformat { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
