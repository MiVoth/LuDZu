using System;

namespace LmmPlanner.Entities.Interfaces;
public interface IFormFillerService
{
    void Export(DateTime from, DateTime to);
}