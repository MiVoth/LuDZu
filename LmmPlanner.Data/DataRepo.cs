using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LmmPlanner.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LmmPlanner.Data;

public interface IDataRepo
{
    Task<List<LmmAssignmentInfo>> GetAllAssignmentsOfPerson(long personId);
    Task<List<LmmPerson>> GetAllPersons();
    Task<List<LmmPerson>> GetAllPersonsForDate(DateTime date);
    Task<LmmPerson> GetPerson(long personId);
}

public class DataRepo : IDataRepo
{
    private MyContext ctx;

    public DataRepo(MyContext context)
    {
        ctx = context;
    }
    byte[] isTrue() => new byte[1] { (byte)48 };
    byte[] isFalse() => new byte[1] { (byte)49 };
    public async Task<List<LmmPerson>> GetAllPersonsForDate(DateTime date)
    {
        List<LmmPerson> persons = await GetAllPersons();
        List<long?> personids = persons.Select(d => (long?)d.Id).ToList();
        List<long> unavail = await ctx.Unavailables.Where(d =>
        d.StartDate <= date && d.EndDate >= date
        && personids.Contains(d.PersonId)).Select(d => d.PersonId ?? 0).ToListAsync();
        return persons.Where(p => !unavail.Contains(p.Id)).ToList();
    }
    public async Task<List<LmmPerson>> GetAllPersons()
    {
        var li = await ctx.Persons
        .Where(p => p.Active == true)
        .Select(p => new LmmPerson
        {
            Id = p.Id,
            IsElder = p.Elder,
            IsServant = p.Servant,
            // Active = p.Active,
            CongregationId = p.CongregationId,
            Firstname = p.Firstname,
            Lastname = p.Lastname,
            Gender = p.Gender,
            UseFor = p.Usefor,
            LastAssignmentDb = p.AssignmentsAsMain.OrderByDescending(d => d.Date).FirstOrDefault().Date,
            LastAssignmentIds = p.AssignmentsAsMain.OrderByDescending(d => d.Date).Take(3).Select(d => d.Id),

            // LastAssignments = p.AssignmentsAsMain.OrderByDescending(d => d.Id).Take(3)
            // .Select(a => new LmmPersonAssignment {
            //     Main = a.MainPerson.Firstname + " " + a.MainPerson.Lastname,
            //     Assist = a.AssistantPerson.Firstname + " " + a.AssistantPerson.Lastname,
            //     Date = a.Date,
            //     Theme = a.LmmSchedule.Theme
            // }).ToList()
            // p.Usefor
        }).ToListAsync();
        var ids = li.SelectMany(s => s.LastAssignmentIds).ToList();
        var lmmAssign = await ctx.LmmAssignments.Where(d => ids.Contains(d.Id))
            .Select(a => new LmmPersonAssignment
            {
                Main = a.MainPerson.Firstname + " " + a.MainPerson.Lastname,
                Assist = a.AssistantPerson.Firstname + " " + a.AssistantPerson.Lastname,
                Volu = a.VolunteerPerson.Firstname + " " + a.VolunteerPerson.Lastname,
                Date = a.Date,
                Theme = a.LmmSchedule.Theme,
                AssigneeId = a.AssigneeId,
                VolunteerId = a.VolunteerId,
                AssistantId = a.AssistantId
            }).ToListAsync();
        foreach (var item in li)
        {
            item.LastAssignments = lmmAssign.Where(l =>
                l.AssigneeId == item.Id || l.VolunteerId == item.Id || l.AssistantId == item.Id)
                .OrderByDescending(l => l.Date)
                .ToList();
        }
        return li.Where(l => l.IsActive).ToList();
    }

    public async Task<LmmPerson> GetPerson(long personId)
    {
        var li = await ctx.Persons
        .Where(p => p.Id == personId)
        .Select(p => new LmmPerson
        {
            Id = p.Id,
            // Active = p.Active,
            CongregationId = p.CongregationId,
            Firstname = p.Firstname,
            Lastname = p.Lastname,
            Gender = p.Gender,
            UseFor = p.Usefor,
            // LastAssignment = p.AssignmentsAsMain.OrderByDescending(d => d.Id).FirstOrDefault().LmmScheduleId
            // p.Usefor
        }).FirstOrDefaultAsync() ?? new();
        return li;
    }

    public async Task<List<LmmAssignmentInfo>> GetAllAssignmentsOfPerson(long personId)
    {
        var li = await ctx.LmmAssignments
        .Where(p => p.AssigneeId == personId || p.VolunteerId == personId || p.AssistantId == personId)
        .OrderByDescending(d => d.Date)
        .Select(p => new LmmAssignmentInfo
        {
            Id = p.Id,
            MainName = p.MainPerson == null ? "" : p.MainPerson.Firstname + " " + p.MainPerson.Lastname,
            VolName = p.VolunteerPerson == null ? "" : p.VolunteerPerson.Firstname + " " + p.VolunteerPerson.Lastname,
            AssiName = p.AssistantPerson == null ? "" : p.AssistantPerson.Firstname + " " + p.AssistantPerson.Lastname,
            AssignDate = p.LmmSchedule == null ? "" : p.LmmSchedule.Date.ToString(),
            Theme = p.LmmSchedule == null ? "" : p.LmmSchedule.Theme,
            Source = p.LmmSchedule == null ? "" : p.LmmSchedule.Source,
            StudyNumber = p.LmmSchedule == null ? null : p.LmmSchedule.StudyNumber,
            TalkId = p.LmmSchedule == null ? null : p.LmmSchedule.TalkId
            // Active = p.Active,
            // CongregationId = p.CongregationId,
            // Firstname = p.Firstname,
            // Lastname = p.Lastname,
            // Gender = p.Gender,
            // p.Usefor
        }).ToListAsync();
        return li;
    }
    //dotnet ef dbcontext scaffold "data source=Z:\FritzUSB\Dokumente\Michi\Theokratie\theocbase_backup.sqlite" Microsoft.EntityFrameworkCore.Sqlite --output-dir TheocData
}