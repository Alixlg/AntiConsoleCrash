using AntiCrash;

internal class Program
{
    private async static Task Main(string[] args)
    {
        Console.Clear();
        while (true)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter the name of the program : ");
                string programName = Console.ReadLine() ?? "";

                Console.Write("Enter program Path : ");
                string programPath = Console.ReadLine() ?? "";

                Console.Write("If your program has a restart.bat file, enter its path, if not leave this field blank : ");
                string batPath = Console.ReadLine() ?? "";

                Console.ResetColor();

                if (programName == "" || programPath == "")
                {
                    Console.WriteLine("Error : Shoma nmitavanid name ya address barname ra ba field khaali por konid !");
                    continue;
                }

                while (true)
                {
                    if (!Tools.IsProcessRunning(programName) && !Tools.IsBatRunningFromPath(batPath))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Program is closed trying to running again . . .");
                        Console.ResetColor();
                        Tools.StartProcess(programPath);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Program is running !");
                        Console.ResetColor();
                    }

                    Tools.KillOneInstance(programName);
                    await Task.Delay(2000);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error : {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}