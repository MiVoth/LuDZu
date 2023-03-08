using System.Collections.Generic;

namespace LmmPlanner.Entities.Models
{
    public static class PartTypeStatics
    {
        public static PartTypeStatic Prayer = new PartTypeStatic { TypeValue = PartType.Prayer, Kuerzel = "Gebet" };
        public static PartTypeStatic Chair = new PartTypeStatic { TypeValue = PartType.Chair, Kuerzel = "Vors." };
        public static PartTypeStatic FirstTalk = new PartTypeStatic(20, 30, "Vortrag") { TypeValue = PartType.FirstTalk };
        public static PartTypeStatic Treasures = new PartTypeStatic(30, 40, "Schätze") { TypeValue = PartType.Treasures };
        public static PartTypeStatic BibleReading = new PartTypeStatic(40, 50, "Bibelles.") {TypeValue = PartType.BibleReading };
        public static PartTypeStatic ImproveVideo = new PartTypeStatic(50, 60, "Video") { TypeValue = PartType.ImproveVideo };
        public static PartTypeStatic InitialCall = new PartTypeStatic(60, 70, "ErstG") { TypeValue = PartType.InitialCall };
        public static PartTypeStatic ReturnVisit = new PartTypeStatic(70, 100, "RB") { TypeValue = PartType.ReturnVisit };
        public static PartTypeStatic BibleStudy = new PartTypeStatic(100, 110, "HB") { TypeValue = PartType.BibleStudy };
        public static PartTypeStatic Talk = new PartTypeStatic(110, 120, "Kurzvortr.") { TypeValue = PartType.Talk };
        public static PartTypeStatic LifePart = new PartTypeStatic(120, 140, "Leben-Dienst") { TypeValue = PartType.LifePart };
        public static PartTypeStatic CongregationStudy = new PartTypeStatic(140, 160, "VBS") { TypeValue = PartType.CongregationStudy };
        public static List<PartTypeStatic> PartTypeList = new() {
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
        };
    }
}