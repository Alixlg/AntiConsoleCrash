using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace AntiCrash
{
    public class Tools
    {
        public static bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }
        public static void StartProcess(string processPath)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = processPath,
                    UseShellExecute = true,
                    CreateNoWindow = false
                });
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The program was successfully implemented.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error problem running the program :{ex.Message}");
                Console.ResetColor();
            }
        }
        public static bool IsBatRunningFromPath(string batPath)
        {
            if (batPath == "")
            {
                return false;
            }

            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE Name='cmd.exe'"))
                {
                    foreach (ManagementObject process in searcher.Get())
                    {
                        string commandLine = process["CommandLine"]?.ToString() ?? "";

                        if (commandLine.Contains(batPath, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error : {ex.Message}");
                Console.ResetColor();
            }

            return false;
        }
        public static void KillOneInstance(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);

            if (processes.Length > 1)
            {
                try
                {
                    Process processToKill = processes.OrderByDescending(p => p.StartTime).FirstOrDefault();

                    if (processToKill != null)
                    {
                        processToKill.Kill();

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Process {processToKill.Id} is closed.");
                        Console.ResetColor();
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error to closing the program : {ex.Message}");
                    Console.ResetColor();
                }
            }
        }
    }
}