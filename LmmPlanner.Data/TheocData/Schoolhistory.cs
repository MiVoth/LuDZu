using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Schoolhistory
    {
        public long? StudentId { get; set; }
        public long? AssistantId { get; set; }
        public long? VolunteerId { get; set; }
        public byte[]? Date { get; set; }
        public long? Number { get; set; }
        public long? Classnumber { get; set; }
    }
}
