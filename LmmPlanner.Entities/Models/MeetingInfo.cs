using System;
using System.Collections.Generic;
using System.Linq;
using LmmPlanner.Entities.Enums;

namespace LmmPlanner.Entities.Models
{
    public class MeetingInfo
    {
        public long MeetingId { get; set; }
        public string? BibleReading { get; set; }
        public long? ChairmanId { get; set; }
        public string Chairman { get; set; } = string.Empty;
        public long? SongEnd { get; set; }
        public long? SongMiddle { get; set; }
        public long? SongBeginning { get; set; }
        public string? ClosingComments { get; set; }
        public long? PrayerEndId { get; set; }
        public long? PrayerBeginningId { get; set; } = null;
        public string PrayerBeginning { get; set; } = string.Empty;
        public string PrayerEnd { get; set; } = string.Empty;
        public DateTime? MeetingDate { get; set; }

        public bool MoreClasses { get => AssignmentInfos.Any(d => d.Classnumber > 1); }
        public List<MeetingPartInfo> PartInfos { get; set; } = new();
        public List<MeetingAssignmentInfo> AssignmentInfos { get; set; } = new();
        public IEnumerable<MeetingPartInfo> TreasureParts { get => PartInfos.Where(d => d.TreasurePart); }
        public IEnumerable<MeetingPartInfo> ServiceParts { get => PartInfos.Where(d => d.ServicePart); }
        public IEnumerable<MeetingPartInfo> LifeParts { get => PartInfos.OrderBy(d => d.RowOrder).ThenBy(d => d.TalkId).Where(d => d.LifePart); }
        public IEnumerable<MeetingPartInfo> OtherParts { get => PartInfos.Where(d => !d.TreasurePart && !d.ServicePart && !d.LifePart); }
        public string Alert { get; set; } = string.Empty;
        public ExceptionVariant ExceptionVariant { get; set; } = ExceptionVariant.NoException;
        public bool IsServiceWeek { get => ExceptionVariant == ExceptionVariant.ServiceWeek; }
        public string? CircuitOverseer { get; set; }

        public MeetingAssignmentInfo GetAssignmentToPart(long scheduleId)
        {
            return AssignmentInfos.FirstOrDefault(a => a.ScheduleId == scheduleId) ?? new();
        }
    }

    public class MeetingPartInfo
    {
        public string? Theme { get; set; }
        public long? TalkId { get; set; }
        public long? TalkMeetingSection { get; set; }
        public long? TalkMeeting { get; set; }

        public bool TreasurePart { get => IsTreasurePart(TalkId); }
        public bool ServicePart { get => IsServicePart(TalkId); }
        public bool LifePart { get => IsLifePart(TalkId); }
        public string? Source { get; set; }
        public long Id { get; set; }
        public long? Time { get; set; }
        public long? RowOrder { get; set; }

        public static bool IsTreasurePart(long? talkId)
        {
            return (new List<long?> { 10, 20, 30, 40 }).Contains(talkId);
        }
        public static bool IsServicePart(long? talkId)
        {
            var talky = talkId ?? 0;
            return PartTypeStatics.InitialCall.IsBetween(talky)
            || PartTypeStatics.ReturnVisit.IsBetween(talky)
            || PartTypeStatics.BibleStudy.IsBetween(talky)
            || PartTypeStatics.Talk.IsBetween(talky)
            || PartTypeStatics.ImproveVideo.IsBetween(talky)
            ;
            // return (new List<long?> { 50, 60, 70, 80, 90, 100, 110, 170, 270, 280 }).Contains(talkId);
        }
        public static bool IsLifePart(long? talkId)
        {
            var talky = talkId ?? 0;
            return PartTypeStatics.LifePart.IsBetween(talky)
                || PartTypeStatics.CongregationStudy.IsBetween(talky);
            // return (new List<long?> { 120, 130, 140, 150, 160 }).Contains(talkId);
        }

        // public static bool IsBibleReading(long talkId) => talkId >= 60 && talkId < 70;
        // public static bool IsInitialCall(long talkId) => talkId >= 60 && talkId < 70;
        // public static bool IsReturnVisit(long talkId) => talkId >= 70 && talkId < 100;
        // public static bool IsBibleStudy(long talkId) => talkId >= 70 && talkId < 100;

        public static PartType GetPartType(long? talkId)
        {
            if (talkId == null)
            {
                throw new Exception("No TalkId");
            }
            long id = talkId.Value;
            if (id >= 20 && id < 30)
            {
                return PartType.FirstTalk;
            }
            else if (id >= 30 && id < 40)
            {
                return PartType.Treasures;
            }
            else if (id >= 40 && id < 50)
            {
                return PartType.BibleReading;
            }
            else if (id >= 50 && id < 60)
            {
                return PartType.ImproveVideo;
            }
            else if (id >= 60 && id < 70)
            {
                return PartType.InitialCall;
            }
            else if (id >= 70 && id < 100)
            {
                return PartType.ReturnVisit;
            }
            else if (id >= 100 && id < 110)
            {
                return PartType.BibleStudy;
            }
            else if (id >= 110 && id < 120)
            {
                return PartType.Talk;
            }
            else if (id >= 120 && id < 140)
            {
                return PartType.LifePart;
            }
            else if (id >= 140 && id < 160)
            {
                return PartType.CongregationStudy;
            }
            return PartType.Prayer;
        }
    }
    public class PartTypeStatic
    {
        public PartTypeStatic()
        {

        }
        public PartTypeStatic(int min, int max, string kuerzel)
        {
            Kuerzel = kuerzel;
            TypeIdMin = min;
            TypeIdMax = max;
        }
        public PartType TypeValue { get; set; }
        public int TypeIdMin { get; set; }
        public int TypeIdMax { get; set; }
        public string Kuerzel { get; set; } = "?";
        public bool IsBetween(long val) => val >= TypeIdMin && val < TypeIdMax;
    }

    public class MeetingAssignmentInfo
    {
        public long AssignmentId { get; set; }
        public long? ScheduleId { get; set; }
        public string AssistantPerson { get; set; } = string.Empty;
        public string MainPerson { get; set; } = string.Empty;
        public long? Classnumber { get; set; }
    }

    public class FittingPersons
    {
        public long ScheduleId { get; set; }
        public long? TalkId { get; set; }
        public PartType PartInfo { get; set; }
        public List<LmmPerson> Persons { get; set; } = new();
        public string? Theme { get; set; }
        public bool Assist { get; set; }
        public long? AssignmentId { get; set; }
    }
}