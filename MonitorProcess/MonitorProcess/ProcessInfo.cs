using System;

public class ProcessInfo
{  
        public string ProcessName { get; }
        public double TotalLifeTimeInMinutes { get; }

        public ProcessInfo(string processName, int totalLifeTimeInMinutes)
        {
            ProcessName = processName;
            TotalLifeTimeInMinutes = totalLifeTimeInMinutes;
        }
}

