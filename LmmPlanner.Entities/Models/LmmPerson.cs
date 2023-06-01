using System;
using System.Collections.Generic;
using System.Linq;

namespace LmmPlanner.Entities.Models
{
    public class UnavailableInfo
    {
        public long Id { get; set; }
        public long? PersonId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Active { get; set; }
    }
    public class LmmPersonExtented : LmmPerson
    {
        public List<UnavailableInfo> NotAvailableAt { get; set; } = new();
        public bool IsNotAvailable(AssignmentOverviewMeeting meeting) {
            DateTime date = meeting.Date;
            return NotAvailableAt.Any(n => date >= n.StartDate && date<= n.EndDate); 
        } 
    }
    public class LmmPerson
    {
        public long Id { get; set; }
        // public bool? Active { get; set; }
        public long? CongregationId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string Name { get => $"{Firstname} {Lastname}"; }
        public string? Gender { get; set; }
        // public DateTime? LastAssignmentDb { get; set; }
        public DateTime? LastAssignment { get => LastAssignments.OrderByDescending(d => d.Date).FirstOrDefault()?.Date; }
        public long? UseFor { get; set; }
        public string UseForString
        {
            get
            {
                long number = UseFor ?? 0;
                var by = Convert.ToString(number, 2);
                return by.PadLeft(32, '0');
                // string binary = Enumerable.Range(0, (int)Math.Log(number, 2) + 1).Aggregate(string.Empty, (collected, bitshifts) => ((number >> bitshifts) & 1) + collected);
                // return binary;
            }
        }
        public bool MayTakePart(PartType type)
        {
            switch (type)
            {
                case PartType.Prayer:
                    return IsPrayer;
                case PartType.Chair:
                    return IsLmmChair;
                case PartType.FirstTalk:
                    return IsLmmTalk;
                case PartType.Treasures:
                    return IsTreasurePart;
                case PartType.BibleReading:
                    return IsBibleReader;
                case PartType.ImproveVideo:
                    return IsVideoPart;
                case PartType.InitialCall:
                    return IsInitialCall;
                case PartType.ReturnVisit:
                    return IsReturnVisit;
                case PartType.BibleStudy:
                    return IsBibleStudy;
                case PartType.Talk:
                    return IsLmmTalk;
                case PartType.LifePart:
                    return IsVideoPart;
                case PartType.CongregationStudy:
                    return IsCongregationStudy;
                default:
                    return false;
            }
        }

        public bool IsActive { get => UseForString[27] == '0'; }
        public bool IsPublicTalkChair { get => UseForString[25] == '1'; }
        public bool IsWtReader { get => UseForString[24] == '1'; }
        public bool IsPartner { get => UseForString[23] == '1'; }
        public bool IsPublicTalk { get => UseForString[22] == '1'; }
        public bool IsCongregationStudy { get => UseForString[21] == '1'; }
        public bool IsPrayer { get => UseForString[20] == '1'; }
        public bool IsCongregationStudyReader { get => UseForString[18] == '1'; }
        public bool IsWtHost { get => UseForString[17] == '1'; }
        public bool OnlyMainHall { get => UseForString[16] == '1'; }
        public bool OnlySecondHall { get => UseForString[15] == '1'; }
        public bool IsLmmChair { get => UseForString[14] == '1'; }
        public bool IsTreasurePart { get => UseForString[12] == '1'; }
        public bool IsBibleReader { get => UseForString[11] == '1'; }
        public bool IsInitialCall { get => UseForString[9] == '1'; }
        public bool IsReturnVisit { get => UseForString[8] == '1'; }
        public bool IsBibleStudy { get => UseForString[7] == '1'; }
        public bool IsLmmTalk { get => UseForString[6] == '1'; }
        public bool IsInvitationHost { get => UseForString[5] == '1'; }
        public bool IsStudyTalk { get => UseForString[4] == '1'; }
        public bool IsVideoPart { get => UseForString[3] == '1'; }
        public List<LmmPersonAssignment> LastAssignments { get; set; } = new();
        public IEnumerable<long>? LastAssignmentIds { get; set; } = new List<long>();
        public bool IsElder { get; set; }
        public bool IsServant { get; set; }
    }

    public class LmmPersonAssignment
    {
        public string Main { get; set; } = string.Empty;
        public string Assist { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
        public string? Theme { get; set; }
        public long? AssigneeId { get; set; }
        public long? VolunteerId { get; set; }
        public long? AssistantId { get; set; }
        public string Volu { get; set; } = string.Empty;
    }
}