using System;
using System.Collections.Generic;

namespace LmmPlanner.Data.TheocData
{
    public partial class Person
    {
        public virtual ICollection<LmmAssignment>? AssignmentsAsMain { get; set; } = new List<LmmAssignment>();
        public virtual ICollection<LmmAssignment> AssignmentsAsVolunteer { get; set; } = new List<LmmAssignment>();
        public virtual ICollection<LmmAssignment> AssignmentsAsAssistant { get; set; } = new List<LmmAssignment>();
        public virtual ICollection<LmmMeeting> AssignmentsAsChairman { get; set; } = new List<LmmMeeting>();
        public virtual ICollection<LmmMeeting> AssignmentsAsPrayerBeginning { get; set; } = new List<LmmMeeting>();
        public virtual ICollection<LmmMeeting> AssignmentsAsPrayerEnd { get; set; } = new List<LmmMeeting>();
    }

    public partial class LmmAssignment
    {
        public virtual Person? MainPerson { get; set; }
        public virtual Person? VolunteerPerson { get; set; }
        public virtual Person? AssistantPerson { get; set; }

        public virtual LmmSchedule? LmmSchedule { get; set; }
    }

    public partial class LmmMeeting
    {
        public virtual Person? ChairmanPerson { get; set; }
        public virtual Person? PrayerBeginningPerson { get; set; }
        public virtual Person? PrayerEndPerson { get; set; }
    }

    public partial class LmmSchedule
    {
        public virtual TalkInfo? TalkInfo { get; set; }

        public virtual ICollection<LmmAssignment> Assignments { get; set; } = new List<LmmAssignment>();
    }

    public partial class TalkInfo
    {
        public virtual ICollection<LmmSchedule> Assignments { get; set; } = new List<LmmSchedule>();
    }

    // public partial class LmmSchedule
    // {
    //     public virtual ICollection<LmmAssignment> AssignmentsAsAssistant { get; set; } = new List<LmmAssignment>();
    // }
}
