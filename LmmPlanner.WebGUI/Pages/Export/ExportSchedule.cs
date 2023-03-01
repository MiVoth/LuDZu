using System;
using System.Collections.Generic;

namespace LmmPlanner.WebGUI.Export
{
    public class ScheduleExport
    {
        public string? MeetingName { get; internal set; }
        public string? CongregationTitle { get; internal set; }
        public int? MwbNo { get; internal set; }
        public DateTime? MeetingDate { get; internal set; }
        public string? Song1Starttime { get; internal set; }
        public string? BeginningTime { get; internal set; }
        public string? ConclusionTime { get; internal set; }
        public string? EndTime { get; internal set; }
        public long? SongBeginning { get; internal set; }
        public string? PrayerBeginning { get; internal set; }
        public long? SongMiddle { get; internal set; }
        public long? SongEnd { get; internal set; }
        public string? PrayerEnd { get; internal set; }
        public string? ChairPerson { get; internal set; }
        public string? ChairName { get; internal set; }
        public string? SongName { get; internal set; }
        public string? PrayerName { get; internal set; }
        public string? OpeningName { get; internal set; }
        public string? TreasureName { get; internal set; }
        public string? ServiceName { get; internal set; }
        public string? LifeName { get; internal set; }
        public string? LeaderName { get; internal set; }
        public string? ReaderName { get; internal set; }
        public string? ConclusionName { get; internal set; }
        public string? WeekSource { get; internal set; }
        public string? MWB_COLOR { get; internal set; }
        public string? MWB_COLOR_LIGHT { get; internal set; }
        public IEnumerable<LifeExport> LifeExport { get; internal set; } = new List<LifeExport>();
        public IEnumerable<ServiceExport> ServiceExport { get; internal set; } = new List<ServiceExport>();
        public IEnumerable<TreasureExport> TreasureExport { get; internal set; } = new List<TreasureExport>();
    }

    public class LifeExport
    {
        public string? LifeTime { get; internal set; }
        public long? LifeTime2 { get; internal set; }
        public string? LifeTheme { get; internal set; }
        public string? LifeMain { get; internal set; }
        public string? LifeAssist { get; internal set; }
    }
    public class TreasureExport
    {
        public string? TreasureTime { get; internal set; }
        public string? TreasureTheme { get; internal set; }
        public string? TreasureMain { get; internal set; }
        public string? TreasureAssist { get; internal set; }
    }
    public class ServiceExport
    {
        public string? AssignTime { get; internal set; }
        public string? AssignTheme { get; internal set; }
        public string? AssignMain { get; internal set; }
        public string? AssignAssist { get; internal set; }
    }
}