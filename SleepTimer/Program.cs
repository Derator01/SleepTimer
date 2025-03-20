using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TimedSleep;

public static partial class Program
{
    [LibraryImport("Powrprof.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool SetSuspendState([MarshalAs(UnmanagedType.Bool)] bool hibernate, [MarshalAs(UnmanagedType.Bool)] bool forceCritical, [MarshalAs(UnmanagedType.Bool)] bool disableWakeEvent);

    private static void Main(string[] arg)
    {
        if (arg[0] == "a")
        {
            Process[] targetProcesses = Process.GetProcessesByName("TimedSleep");
            foreach (var process in targetProcesses)
                process.Kill();
            return;
        }

        Thread.Sleep(int.Parse(arg[0]));

        SetSuspendState(false, true, false);
    }
}