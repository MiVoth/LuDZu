using System;
using System.Collections.Generic;

namespace LmmPlanner.Entities.Models
{
    public class ScheduleExport
    {
        public string? MeetingName { get; set; }
        public string? CongregationTitle { get; set; }
        public int? MwbNo { get; set; }
        public DateTime? MeetingDate { get; set; }
        public string? Song1Starttime { get; set; }
        public string? BeginningTime { get; set; }
        public string? ConclusionTime { get; set; }
        public string? EndTime { get; set; }
        public long? SongBeginning { get; set; }
        public string? PrayerBeginning { get; set; }
        public long? SongMiddle { get; set; }
        public long? SongEnd { get; set; }
        public string? PrayerEnd { get; set; }
        public string? ChairPerson { get; set; }
        public string? ChairName { get; set; }
        public string? SongName { get; set; }
        public string? PrayerName { get; set; }
        public string? OpeningName { get; set; }
        public string? TreasureName { get; set; }
        public string? ServiceName { get; set; }
        public string? LifeName { get; set; }
        public string? LeaderName { get; set; }
        public string? ReaderName { get; set; }
        public string? ConclusionName { get; set; }
        public string? WeekSource { get; set; }
        public string? MWB_COLOR { get; set; }
        public string? MWB_COLOR_LIGHT { get; set; }
        public IEnumerable<LifeExport> LifeExport { get; set; } = new List<LifeExport>();
        public IEnumerable<ServiceExport> ServiceExport { get; set; } = new List<ServiceExport>();
        public IEnumerable<TreasureExport> TreasureExport { get; set; } = new List<TreasureExport>();
        public string? Alert { get; set; }
        public bool ShowSchedule { get; set; }
    }

    public class LifeExport
    {
        public string? LifeTime { get; set; }
        public long LifeTime2 { get; set; }
        public string? LifeTheme { get; set; }
        public string? LifeMain { get; set; }
        public string? LifeAssist { get; set; }
    }
    public class TreasureExport
    {
        public string? TreasureTime { get; set; }
        public string? TreasureTheme { get; set; }
        public string? TreasureMain { get; set; }
        public string? TreasureAssist { get; set; }
        public long TreasureLength { get; set; }
    }
    public class ServiceExport
    {
        public string? AssignTime { get; set; }
        public string? AssignTheme { get; set; }
        public string? AssignMain { get; set; }
        public string? AssignAssist { get; set; }
        public long AssignLength { get; set; }
    }
}