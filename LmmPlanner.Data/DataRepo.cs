﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LmmPlanner.Entities.Interfaces;
using LmmPlanner.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LmmPlanner.Data;

public class DataRepo : IDataRepo
{
    private IMapper _mapper;
    private MyContext ctx;

    public DataRepo(MyContext context,
        IMapper mapper)
    {
        _mapper = mapper;
        ctx = context;
    }
    byte[] isTrue() => new byte[1] { (byte)48 };
    byte[] isFalse() => new byte[1] { (byte)49 };
    public async Task<List<LmmPerson>> GetAllPersonsForDate(DateTime date)
    {
        string? schooldaay = ctx.Settings.Where(s => s.Name == "school_day").Select(d => d.Value).FirstOrDefault();
        if (int.TryParse($"{schooldaay}", out var sd))
        {
            if ((int)date.DayOfWeek != sd)
            {
                date = date.AddDays(sd - (int)date.DayOfWeek);
            }
        }
        DateTime dateStart = date.AddDays(-1);
        DateTime dateEnd = date.AddDays(1);

        List<LmmPerson> persons = await GetAllPersons();
        List<long?> personids = persons.Select(d => (long?)d.Id).ToList();
        List<long> unavail = await ctx.Unavailables.Where(d =>
        ((d.StartDate <= date && d.EndDate >= date)
        || (d.StartDate > dateStart && d.EndDate < dateEnd))
        && personids.Contains(d.PersonId)).Select(d => d.PersonId).ToListAsync();
        return persons.Where(p => !unavail.Contains(p.Id)).ToList();
    }
    public async Task<List<LmmPersonExtented>> GetAllPersonsWithUnavailable()
    {
        var persons = await GetAllPersons();
        var personIds = persons.Select(f => f.Id).ToList();

        var unav = await ctx.Unavailables.Where(u => personIds.Contains(u.PersonId)).ToListAsync();
        var ext = new List<LmmPersonExtented>();
        foreach (var person in persons)
        {
            var extPerson = _mapper.Map<LmmPersonExtented>(person);
            extPerson.NotAvailableAt = _mapper.ProjectTo<UnavailableInfo>(unav.Where(p => p.PersonId == extPerson.Id).AsQueryable()).ToList();
            ext.Add(extPerson);
            
        }
        return ext;
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
            // LastAssignmentDb = p.AssignmentsAsMain != null ? p.AssignmentsAsMain.OrderByDescending(d => d.Date).Select(d => d.Date).FirstOrDefault() : null,
            // LastAssignmentDb = p.AssignmentsAsMain.OrderByDescending(d => d.Date).Select(d => d.Date).FirstOrDefault(),
            // LastAssignmentIds = p.AssignmentsAsMain.OrderByDescending(d => d.Date).Take(3).Select(d => d.Id),
            LastAssignmentIds = p.AssignmentsAsMain != null ? p.AssignmentsAsMain.OrderByDescending(d => d.Date).Take(3).Select(d => d.Id) : null,

            // LastAssignments = p.AssignmentsAsMain.OrderByDescending(d => d.Id).Take(3)
            // .Select(a => new LmmPersonAssignment {
            //     Main = a.MainPerson.Firstname + " " + a.MainPerson.Lastname,
            //     Assist = a.AssistantPerson.Firstname + " " + a.AssistantPerson.Lastname,
            //     Date = a.Date,
            //     Theme = a.LmmSchedule.Theme
            // }).ToList()
            // p.Usefor
        }).OrderBy(d => d.Lastname).ThenBy(d => d.Firstname).ToListAsync();
        var ids = li.SelectMany(s => s.LastAssignmentIds).ToList();
        var lmmAssign = await ctx.LmmAssignments.Where(d => ids.Contains(d.Id))
            .Select(a => new LmmPersonAssignment
            {
                Main = a.MainPerson == null ? "" : a.MainPerson.Firstname + " " + a.MainPerson.Lastname,
                Assist = a.AssistantPerson == null ? "" : a.AssistantPerson.Firstname + " " + a.AssistantPerson.Lastname,
                Volu = a.VolunteerPerson == null ? "" : a.VolunteerPerson.Firstname + " " + a.VolunteerPerson.Lastname,
                Date = a.Date,
                Theme = a.LmmSchedule == null ? "" : a.LmmSchedule.Theme,
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

    public async Task<List<PersonNotAvailable>> GetPersonNotAvailableAsync(long personId)
    {
        var li = await ctx.Unavailables
        .Where(p => p.PersonId == personId)
        .OrderBy(d => d.StartDate).ThenBy(d => d.EndDate)
        .Select(p => new PersonNotAvailable
        {
            Id = p.Id,
            PersonId = p.PersonId,
            From = p.StartDate,
            To = p.EndDate,
            Active = p.Active == true
        }).ToListAsync();
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