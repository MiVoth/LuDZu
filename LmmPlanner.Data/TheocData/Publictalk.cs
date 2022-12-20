using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Publictalk
    {
        public long Id { get; set; }
        public long? ThemeNumber { get; set; }
        public string? ThemeName { get; set; }
        public string? Revision { get; set; }
        public byte[]? ReleaseDate { get; set; }
        public byte[]? DiscontinueDate { get; set; }
        public long? LangId { get; set; }
        public long? TimeStamp { get; set; }
        public byte[]? Active { get; set; }
        public string? Uuid { get; set; }
    }
}
