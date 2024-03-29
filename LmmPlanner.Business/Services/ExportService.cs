using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LmmPlanner.Data.TheocData;
using LmmPlanner.Entities.Interfaces;
using LmmPlanner.Entities.Models;
using MiVo.Text.Replacer.Interfaces;
using PuppeteerSharp;

namespace LmmPlanner.Business.Services
{
    public class ExportService : IExportService
    {
        private ISettingsRepo _settingsRepo;
        private IScheduleRepo scheduleRepo;

        public ExportService(ISettingsRepo settingsRepo,
        IScheduleRepo scheduleRepo)
        {
            _settingsRepo = settingsRepo;
            this.scheduleRepo = scheduleRepo;
        }

        public async Task<byte[]> ExportToPdfAsync(DateTime start, DateTime end)
        {

            using var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            // , Args = new[] {
            //     // "--font-render-hinting=none'",
            //     "--force-color-profile=srgb"
            // } });
            await using var page = await browser.NewPageAsync();
            await page.SetUserAgentAsync("Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36");
            // await page.GoToAsync("http://www.google.com"); // In case of fonts being loaded from a CDN, use WaitUntilNavigation.Networkidle0 as a second param.
            // await page.EvaluateExpressionHandleAsync("document.fonts.ready"); // Wait for fonts to be loaded. Omitting this might result in no text rendered in pdf.

            // await using var page = await browser.NewPageAsync();
            string html = await ExportAsync(start, end);
            await page.SetContentAsync(html);
            await page.GetContentAsync();
            return await page.PdfDataAsync(new PdfOptions
            {
                PrintBackground = true,
                // HeaderTemplate = "<p>Hallo<p>",
                Format = PuppeteerSharp.Media.PaperFormat.A4
            });
            // var result = await page.GetContentAsync();

        }
        public async Task<string> ExportAsync()
        {
            DateTime activeDate = DateTime.Now;

            return await ExportAsync(activeDate, activeDate.AddDays(7 * 3));
        }
        // public async Task<string> ExportAsync2(DateTime start, DateTime end)
        // {

        // }


        public async Task<string> ExportAsync(DateTime start, DateTime end)
        {
            // var rpl = new DocumentForger.Replacer(System.IO.File.ReadAllText(@"App_Data\MW-Schedule_1.htm"));
            string template = System.IO.File.ReadAllText(@"App_Data\MW-Schedule_1.htm");
            IReplacer rpl = MiVo.Text.Replacer.ReplacerFactory.GetReplacer(template, new MiVo.Text.Replacer.ReplacerConfig
            {
                ReplacePattern = "\"*{0}*\""
            }); // DocumentForger.Replacer();
            // rpl.ReplacePattern = "\"*{0}*\"";
            // MeetingInfo Meeting = await scheduleRepo.GetSchedule(start); //.GetAllPersons();

            // ScheduleExport exp = await GetExportAsync(Meeting);
            List<ScheduleExport> schedList = new();
            //  {
            //     exp
            // };
            DateTime newDate = start;
            while (newDate < end)
            {
                MeetingInfo Meeting = await scheduleRepo.GetSchedule(newDate); //.GetAllPersons();
                ScheduleExport exp = await GetExportAsync(Meeting);
                schedList.Add(exp);
                newDate = newDate.AddDays(7);
            }

            string color = ((await _settingsRepo.GetSetting(60))?.Value ?? "").Split(",")[start.Month];
            // var color2 = ((await _settingsRepo.GetSetting(60))?.Value ?? "").Split(",");
            int RGBint = Convert.ToInt32(color.Replace("#", ""), 16);
            byte Red = (byte)((RGBint >> 16) & 255);
            byte Green = (byte)((RGBint >> 8) & 255);
            byte Blue = (byte)(RGBint & 255);

            Congregation cong = await _settingsRepo.GetCongregation();
            string html = rpl.GetText(new
            {
                // CongregationTitle = "Leben und Dienst Zusammenkunft",
                CongregationTitle = $"Versammlung {cong.Name}",
                MeetingName = "Zusammenkunft unter der Woche",
                MWB_COLOR = $"rgb({Red},{Green},{Blue})",
                MWB_COLOR_LIGHT = $"rgb({Red},{Green},{Blue}, .3)",
                WeekSched = schedList
            });
            return html;
        }

        public async Task<ScheduleExport> GetExportAsync(MeetingInfo Meeting)
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
            IEnumerable<TreasureExport> treasureParts = Meeting.TreasureParts.OrderBy(f => f.RowOrder).Select(f => new TreasureExport
            {
                TreasureTime = "", // $"{abc(f.Time ?? 0):HH:mm}", // f.Time,
                TreasureTheme = f.Theme,
                TreasureLength = f.Time ?? 0,
                TreasureMain = Meeting.AssignmentInfos.FirstOrDefault(m => m.ScheduleId == f.Id)?.MainPerson,
                TreasureAssist = Meeting.AssignmentInfos.FirstOrDefault(m => m.ScheduleId == f.Id)?.AssistantPerson
            }).ToList();
            DateTime m2 = meetingTime;
            foreach (var item in treasureParts)
            {
                item.TreasureTime = $"{m2:HH:mm}";
                m2 = m2.AddMinutes(item.TreasureLength);
            }

            m2 = m2.AddMinutes(2);
            var serviceParts = Meeting.ServiceParts.OrderBy(f => f.RowOrder).Select(f => new ServiceExport
            {
                AssignTime = $"{abc(1 + f.Time ?? 0):HH:mm}",
                AssignTheme = f.Theme,
                AssignLength = f.Time ?? 0,
                AssignMain = Meeting.AssignmentInfos.FirstOrDefault(m => m.ScheduleId == f.Id)?.MainPerson,
                AssignAssist = Meeting.AssignmentInfos.FirstOrDefault(m => m.ScheduleId == f.Id)?.AssistantPerson
            }).ToList();
            foreach (var item in serviceParts)
            {
                item.AssignTime = $"{m2:HH:mm}";
                m2 = m2.AddMinutes(item.AssignLength + 1);
            }

            m2 = m2.AddMinutes(4);
            // i += 5;
            var lifeParts = Meeting.LifeParts.OrderBy(f => f.RowOrder).Select(f => new LifeExport
            {
                LifeTime = $"{abc(f.Time ?? 0):HH:mm}",
                LifeTime2 = f.Time ?? 0,
                LifeTheme = f.Theme,
                LifeMain = Meeting.AssignmentInfos.FirstOrDefault(m => m.ScheduleId == f.Id)?.MainPerson,
                LifeAssist = Meeting.AssignmentInfos.FirstOrDefault(m => m.ScheduleId == f.Id)?.AssistantPerson
            }).ToList();
            foreach (var item in lifeParts)
            {
                item.LifeTime = $"{m2:HH:mm}";
                m2 = m2.AddMinutes(item.LifeTime2);
            }

            // var lst = lifeParts.Last();
            return new ScheduleExport
            {
                MeetingDate = meetingDate, // Meeting.MeetingDate,
                MwbNo = Meeting.MeetingDate?.Month,
                Song1Starttime = $"{meetingTimeBase:HH:mm}",
                BeginningTime = $"{meetingTimeBase.AddMinutes(5):HH:mm}",
                ConclusionTime = $"{m2:HH:mm}",
                EndTime = $"{m2.AddMinutes(3):HH:mm}",
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
                TreasureExport = treasureParts,
                Alert = Meeting.Alert,
                ShowSchedule = Meeting.ExceptionVariant == Entities.Enums.ExceptionVariant.NoException || Meeting.ExceptionVariant == Entities.Enums.ExceptionVariant.ServiceWeek
            };
        }
    }
}