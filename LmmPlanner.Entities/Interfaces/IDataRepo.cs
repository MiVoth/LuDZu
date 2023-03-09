using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LmmPlanner.Entities.Models;

namespace LmmPlanner.Entities.Interfaces;

public interface IDataRepo
{
    Task<List<LmmAssignmentInfo>> GetAllAssignmentsOfPerson(long personId);
    Task<List<LmmPerson>> GetAllPersons();
    Task<List<LmmPerson>> GetAllPersonsForDate(DateTime date);
    Task<LmmPerson> GetPerson(long personId);

    Task<List<PersonNotAvailable>> GetPersonNotAvailableAsync(long personId);
}