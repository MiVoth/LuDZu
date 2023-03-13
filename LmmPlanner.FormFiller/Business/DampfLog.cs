using System;
using LmmPlanner.LmmFormFiller.Interfaces;

namespace LmmPlanner.LmmFormFiller.Utils
{
    public class DampfLog : IDampfLog
    {
        public DampfLog()
        {
        }

        public void Log(string logText)
        {
            Log(logText, 0);
            // Console.WriteLine(logText);
            // MainWindowViewModel.Instance.CurrentOp = logText;
            // MainWindowViewModel.Instance.Progress = percent;
        }

        public void Log(string logText, decimal percent)
        {
            Console.WriteLine(logText);
        }
    }
}