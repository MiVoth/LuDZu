using System;
using System.Threading.Tasks;
using LmmPlanner.Entities.Models;

namespace LmmPlanner.Entities.Interfaces;

public interface IScheduleRepo
    {
        Task<FittingPersons> GetPersonsToPart(long partId, bool assist);
        Task<MeetingInfo> GetSchedule(DateTime now);
    }