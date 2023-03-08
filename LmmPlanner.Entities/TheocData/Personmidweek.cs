using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Personmidweek
    {
        public long? PersonId { get; set; }
        public byte[]? Date { get; set; }
        public byte[]? Part { get; set; }
        public byte[]? Mtype { get; set; }
    }
}
