using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class TheocbaseSequence
    {
        public long Id { get; set; }
        public byte[]? Name { get; set; }
        public byte[]? Seq { get; set; }
    }
}
