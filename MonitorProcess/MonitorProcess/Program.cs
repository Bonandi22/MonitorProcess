using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Monitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Please specify <program name> <total time (minutes)> <frequency time (minutes)>");
                return;
            }

            var processName = args[0];

            if (!int.TryParse(args[1], out int totalLifeTimeInMinutes))
            {
                Console.WriteLine("Invalid total time (minutes)");
                return;
            }

            if (int.TryParse(args[2], out int frequencyInMinutes) == false)
            {
                Console.WriteLine("frequency time (minutes)");
                return;
            }

            var processInfo = new ProcessInfo(processName, totalLifeTimeInMinutes);
            var timer = new Timer(CheckProcesses, processInfo, 0, frequencyInMinutes * 60 * 1000);

            Console.WriteLine("Checking the processes");
            Console.ReadLine();
        }

        private static void CheckProcesses(object? state)
        {
            var processInfo = state as ProcessInfo;

            Console.WriteLine($"Checking process: {processInfo.ProcessName}");

            var process_list = Process.GetProcesses()
               .Where(p => p.ProcessName == processInfo.ProcessName)
               .ToArray();

            foreach (Process process in process_list)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Process Name: " + process.ProcessName);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Process ID: " + process.Id);

                if (ShouldKillProcess(process, processInfo.TotalLifeTimeInMinutes))
                {
                    process.Kill();
                    Console.WriteLine($"Process Killed: {process.ProcessName}");
                }
            }
        }
        private static bool ShouldKillProcess(Process p, double totalLifeTimeInMinutes)
        {
            return DateTime.Now.Subtract(p.StartTime).TotalMinutes > totalLifeTimeInMinutes;
        }
    }
}

