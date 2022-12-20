using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class TalkInfo
    {
        public long Talkid { get; set; }
        public long? Meeting { get; set; }
        public long? MeetingSection { get; set; }
        public byte[]? CanCounsel { get; set; }
    }
}
