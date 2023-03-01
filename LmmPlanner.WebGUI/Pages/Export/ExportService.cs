using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LmmPlanner.Data;
using LmmPlanner.Data.Entities;
using LmmPlanner.Data.Statistics;
using LmmPlanner.Data.TheocData;

namespace LmmPlanner.WebGUI.Export
{
    public class ExportService
    {
        private ISettingsRepo _settingsRepo;
        private IScheduleRepo scheduleRepo;

        public ExportService(ISettingsRepo settingsRepo,
        IScheduleRepo scheduleRepo)
        {
            _settingsRepo = settingsRepo;
            this.scheduleRepo = scheduleRepo;
        }

        public async Task<string> Export()
        {
            // var rpl = new DocumentForger.Replacer(System.IO.File.ReadAllText(@"App_Data\MW-Schedule_1.htm"));
            string template = System.IO.File.ReadAllText(@"App_Data\MW-Schedule_1.htm");
            var rpl = MiVo.Text.Replacer.ReplacerFactory.GetReplacer(template, new MiVo.Text.Replacer.ReplacerConfig
            {
                ReplacePattern = "\"*{0}*\""
            }); // DocumentForger.Replacer();
            // rpl.ReplacePattern = "\"*{0}*\"";
            DateTime ActiveDate = DateTime.Now;
            var Meeting = await scheduleRepo.GetSchedule(ActiveDate); //.GetAllPersons();

            var exp = await GetExport(Meeting);
            List<ScheduleExport> schedList = new() {
                exp
            };
            Meeting = await scheduleRepo.GetSchedule(ActiveDate.AddDays(7)); //.GetAllPersons();
            exp = await GetExport(Meeting);
            schedList.Add(exp);
            Meeting = await scheduleRepo.GetSchedule(ActiveDate.AddDays(14)); //.GetAllPersons();
            exp = await GetExport(Meeting);
            schedList.Add(exp);
            // rpl.ReplacementRelation.Add("treasure-template", new DocumentForger.Replacement
            // {
            //     Type = DocumentForger.ReplacementType.Template,
            //     Value = exp.TreasureExport.ToList()
            // });
            // rpl.ReplacementRelation.Add("service-template", new DocumentForger.Replacement
            // {
            //     Type = DocumentForger.ReplacementType.Template,
            //     Value = exp.ServiceExport.ToList()
            // });
            // rpl.ReplacementRelation.Add("life-template", new DocumentForger.Replacement
            // {
            //     Type = DocumentForger.ReplacementType.Template,
            //     Value = exp.LifeExport.ToList()
            // });
            var color = ((await _settingsRepo.GetSetting(60))?.Value ?? "").Split(",")[2];

            int RGBint = Convert.ToInt32(color.Replace("#", ""), 16);
            byte Red = (byte)((RGBint >> 16) & 255);
            byte Green = (byte)((RGBint >> 8) & 255);
            byte Blue = (byte)(RGBint & 255);

            string html = rpl.GetText(new
            {
                CongregationTitle = "Leben und Dienst Zusammenkunft",
                MeetingName = "Zusammenkunft unter der Woche",
                MWB_COLOR = $"rgb({Red},{Green},{Blue})",
                MWB_COLOR_LIGHT = $"rgb({Red},{Green},{Blue}, .3)",
                WeekSched = schedList
            });
            return html;
        }

        public async Task<ScheduleExport> GetExport(MeetingInfo Meeting)
        {
            Congregation cong = await _settingsRepo.GetCongregation();
            Setting? schoolDay = await _settingsRepo.GetSetting("school_day");
            int sd = int.Parse(schoolDay?.Value ?? "0");
            DateTime meetingDate = (Meeting.MeetingDate ?? DateTime.Now);
            meetingDate = meetingDate.AddDays(sd - (int)meetingDate.DayOfWeek);
            // var lmmTalk = Meeting.PartInfos
            //     .FirstOrDefault(f => MeetingPartInfo.GetPartType(f.TalkId) == PartType.FirstTalk);
            // var lmmTalkAssign = Meeting.AssignmentInfos.FirstOrDefault(f => f.ScheduleId == lmmTalk?.Id);
            // var lmmTreasure = Meeting.PartInfos
            //     .FirstOrDefault(f => MeetingPartInfo.GetPartType(f.TalkId) == PartType.Treasures);
            // var lmmTreasureAssign = Meeting.AssignmentInfos.FirstOrDefault(f => f.ScheduleId == lmmTalk?.Id);
            // var bibleReading = Meeting.PartInfos
            //     .FirstOrDefault(f => MeetingPartInfo.GetPartType(f.TalkId) == PartType.BibleReading);
            // var bibleReadingAssign = Meeting.AssignmentInfos.FirstOrDefault(f => f.ScheduleId == lmmTalk?.Id);
            DateTime meetingTimeBase = DateTime.Parse(cong.Meeting1Time ?? "");
            DateTime meetingTime = DateTime.Parse(cong.Meeting1Time ?? "").AddMinutes(6);
            long i = 0;
            Func<long, DateTime> abc = f => { var mt = meetingTime.AddMinutes(i); i += f; return mt; };
            var treasureParts = Meeting.TreasureParts.OrderBy(f => f.RowOrder).Select(f => new TreasureExport
            {
                TreasureTime = $"{abc(f.Time ?? 0):HH:mm}", // f.Time,
                TreasureTheme = f.Theme,
                TreasureMain = Meeting.AssignmentInfos.FirstOrDefault(m => m.ScheduleId == f.Id)?.MainPerson,
                TreasureAssist = Meeting.AssignmentInfos.FirstOrDefault(m => m.ScheduleId == f.Id)?.AssistantPerson
            });

            var serviceParts = Meeting.ServiceParts.OrderBy(f => f.RowOrder).Select(f => new ServiceExport
            {
                AssignTime = $"{abc(1 + f.Time ?? 0):HH:mm}",
                AssignTheme = f.Theme,
                AssignMain = Meeting.AssignmentInfos.FirstOrDefault(m => m.ScheduleId == f.Id)?.MainPerson,
                AssignAssist = Meeting.AssignmentInfos.FirstOrDefault(m => m.ScheduleId == f.Id)?.AssistantPerson
            });

            i += 5;
            var lifeParts = Meeting.LifeParts.OrderBy(f => f.RowOrder).Select(f => new LifeExport
            {
                LifeTime = $"{abc(f.Time ?? 0):HH:mm}",
                LifeTime2 = f.Time,
                LifeTheme = f.Theme,
                LifeMain = Meeting.AssignmentInfos.FirstOrDefault(m => m.ScheduleId == f.Id)?.MainPerson,
                LifeAssist = Meeting.AssignmentInfos.FirstOrDefault(m => m.ScheduleId == f.Id)?.AssistantPerson
            });

            var lst = lifeParts.Last();
            return new ScheduleExport
            {
                MeetingDate = meetingDate, // Meeting.MeetingDate,
                MwbNo = Meeting.MeetingDate?.Month,
                Song1Starttime = $"{meetingTimeBase:HH:mm}",
                BeginningTime = $"{meetingTimeBase.AddMinutes(5):HH:mm}",
                ConclusionTime = $"{meetingTimeBase.AddMinutes(i):HH:mm}",
                EndTime = $"{meetingTimeBase.AddMinutes(i + 3):HH:mm}",
                // LmmTalkTime = $"{meetingTime.AddMinutes(6):HH:mm}",
                SongBeginning = Meeting.SongBeginning,
                PrayerBeginning = Meeting.PrayerBeginning,
                SongMiddle = Meeting.SongMiddle,
                SongEnd = Meeting.SongEnd,
                PrayerEnd = Meeting.PrayerEnd,
                ChairPerson = Meeting.Chairman,
                ChairName = "Vorsitz",
                SongName = "Lied",
                PrayerName = "Gebet",
                OpeningName = "Einleitende Worte",
                TreasureName = "Schätze aus Gottes Wort",
                ServiceName = "Uns im Dienst verbessern",
                LifeName = "Unser Leben als Christ",
                LeaderName = "Leiter",
                ReaderName = "Leser",
                ConclusionName = "Schlussworte",
                WeekSource = Meeting.BibleReading,
                LifeExport = lifeParts,
                ServiceExport = serviceParts,
                TreasureExport = treasureParts
            };
        }
    }
}