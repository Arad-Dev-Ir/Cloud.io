namespace Cloudio.Core;

using System.Diagnostics;
using System.Text.RegularExpressions;
using Cloudio.Core.Models;

public class Processor : Model
{
    class WindowsProcess
    {
        public int Id { get; set; }
        public int Port { get; set; }
        public string Protocol { get; set; } = Empty;
    }

    public static void Kill(int port)
    {
        var processes = GetProcesses();
        var any = processes.Any(e => e.Port == port);
        if (any)
            Process
            .GetProcessById(FindProcessId(port))
            .Kill();
    }

    public static int FindProcessId(int port)
    => GetProcesses().First(a => a.Port == port).Id;

    private static List<WindowsProcess> GetProcesses()
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = "netstat.exe",
            Arguments = "-a -n -o",
            WindowStyle = ProcessWindowStyle.Maximized,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        var process = Process.Start(processInfo);
        var soStream = process!.StandardOutput;

        var output = soStream.ReadToEnd();
        if (process.ExitCode != 0)
            throw new Exception("something broken");

        var lines = Regex.Split(output, "\r\n");

        var result = CreateProcessFromOutput(lines);
        return result;
    }

    private static List<WindowsProcess> CreateProcessFromOutput(string[] lines)
    {
        var result = new List<WindowsProcess>();

        foreach (var item in lines)
        {
            if (item.Trim().StartsWith("Proto"))
                continue;

            var parts = item.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var length = parts.Length;
            if (length > 2)
                result.Add(new WindowsProcess
                {
                    Protocol = parts[0],
                    Id = int.Parse(parts[length - 1]),
                    Port = int.Parse(parts[1].Split(':').Last())
                });
        }

        return result;
    }
}