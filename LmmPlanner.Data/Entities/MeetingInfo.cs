using System;
using System.Collections.Generic;
using System.Linq;

namespace LmmPlanner.Data.Entities
{
    public class MeetingInfo
    {
        public string? BibleReading { get; internal set; }
        public long? ChairmanId { get; internal set; }
        public string Chairman { get; internal set; } = string.Empty;
        public long? SongEnd { get; internal set; }
        public long? SongMiddle { get; internal set; }
        public long? SongBeginning { get; internal set; }
        public string? ClosingComments { get; internal set; }
        public long? PrayerEndId { get; internal set; }
        public long? PrayerBeginningId { get; internal set; } = null;
        public string PrayerBeginning { get; internal set; } = string.Empty;
        public string PrayerEnd { get; internal set; } = string.Empty;
        public DateTime? MeetingDate { get; internal set; }

        public bool MoreClasses { get => AssignmentInfos.Any(d => d.Classnumber > 1); }
        public List<MeetingPartInfo> PartInfos { get; set; } = new();
        public List<MeetingAssignmentInfo> AssignmentInfos { get; set; } = new();
        public IEnumerable<MeetingPartInfo> TreasureParts { get => PartInfos.Where(d => d.TreasurePart); }
        public IEnumerable<MeetingPartInfo> ServiceParts { get => PartInfos.Where(d => d.ServicePart); }
        public IEnumerable<MeetingPartInfo> LifeParts { get => PartInfos.Where(d => d.LifePart); }
        public IEnumerable<MeetingPartInfo> OtherParts { get => PartInfos.Where(d => !d.TreasurePart && !d.ServicePart && !d.LifePart); }

        public MeetingAssignmentInfo GetAssignmentToPart(long scheduleId)
        {
            return AssignmentInfos.FirstOrDefault(a => a.ScheduleId == scheduleId) ?? new();
        }
    }

    public class MeetingPartInfo
    {
        public string? Theme { get; internal set; }
        public long? TalkId { get; internal set; }
        public long? TalkMeetingSection { get; internal set; }
        public long? TalkMeeting { get; internal set; }

        public bool TreasurePart { get => IsTreasurePart(TalkId); }
        public bool ServicePart { get => IsServicePart(TalkId); }
        public bool LifePart { get => IsLifePart(TalkId); }
        public string? Source { get; internal set; }
        public long Id { get; internal set; }
        public long? Time { get; internal set; }

        public static bool IsTreasurePart(long? talkId)
        {
            return (new List<long?> { 10, 20, 30, 40 }).Contains(talkId);
        }
        public static bool IsServicePart(long? talkId)
        {
            return (new List<long?> { 50, 60, 70, 80, 90, 100, 110, 170, 270, 280 }).Contains(talkId);
        }
        public static bool IsLifePart(long? talkId)
        {
            return (new List<long?> { 120, 130, 140, 150, 160 }).Contains(talkId);
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
                return PartType.BibleStudy;
            }
            else if (id >= 140 && id < 160)
            {
                return PartType.CongregationStudy;
            }
            return PartType.Prayer;
        }
    }
    public enum PartType
    {
        Prayer,
        Chair,
        FirstTalk,
        Treasures,
        BibleReading,
        ImproveVideo,
        InitialCall,
        ReturnVisit,
        BibleStudy,
        Talk,
        LifePart,
        CongregationStudy
    }

    public class MeetingAssignmentInfo
    {
        public long? ScheduleId { get; internal set; }
        public string AssistantPerson { get; internal set; } = string.Empty;
        public string MainPerson { get; internal set; } = string.Empty;
        public long? Classnumber { get; internal set; }
    }

    public class FittingPersons
    {
        public long? TalkId { get; internal set; }
        public PartType PartInfo { get; internal set; }
        public List<LmmPerson> Persons { get; internal set; } = new();
    }
}