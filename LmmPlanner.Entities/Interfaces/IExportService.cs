using System;
using System.Threading.Tasks;
using LmmPlanner.Entities.Models;

namespace LmmPlanner.Entities.Interfaces;
public interface IExportService
{

    Task<string> ExportAsync();
    Task<string> ExportAsync(DateTime start, DateTime end);
    Task<byte[]> ExportToPdfAsync(DateTime start, DateTime end);
    Task<ScheduleExport> GetExportAsync(MeetingInfo Meeting);
}
