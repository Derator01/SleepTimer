using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ShutdownTimer;

public static partial class Program
{
    private static Regex regex = TimeInput();

    private static void Main()
    {
        int seconds = 0;

        while (seconds < 1)
        {
            Console.WriteLine("Please provide number followed by one of these [s,m,h,d,y].");

            string toParse = Console.ReadLine();

            if (toParse == "a")
            {
                Process.Start(Path.Combine(Directory.GetCurrentDirectory(), @"SleepTimer.exe"), "a");
                return;
            }

            Match match = regex.Match(toParse);

            if (!match.Success)
            {
                Console.WriteLine("Input string was not in a correct format.");
                Main();
                continue;
            }

            seconds = int.Parse(match.Groups["count"].Value) * (match.Groups["type"].Value switch
            {
                "s" => 1,
                "m" => 60,
                "h" => 3600,
                "d" => 86400,
                "y" => 31536000,
                _ => 1
            }) * 1000;
        }

        Process.Start(Path.Combine(Directory.GetCurrentDirectory(), @"SleepTimer.exe"), seconds.ToString());
    }

    [GeneratedRegex(@"^(?<count>\d+)(?<type>[smhdy])")]
    private static partial Regex TimeInput();
}