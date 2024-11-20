namespace Cloudio.Web.Services.Hosting;

using System.Diagnostics;
using Cloudio.Core;

public class HostService(HostOptions options) : IStartableHost
{
    public string BaseUrl => $"http://localhost:{_options.Port}";
    public HostOptions _options = options;
    private readonly AutoResetEvent _event = new(false);

    public void Up()
    {
        Processor.Kill(_options.Port);
        var info = new ProcessStartInfo("dotnet")
        {
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            Arguments = $"run --project \"{_options.ProjectPath}\"",
        };
        var process = Process.Start(info);

        process!.ErrorDataReceived += (s, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
                throw new Exception(e.Data);
        };
        process.OutputDataReceived += (s, e) =>
        {
            if (e.Data != null && e.Data.Contains("Now listening on", StringComparison.OrdinalIgnoreCase))
                _event.Set();
        };

        process.BeginErrorReadLine();
        process.BeginOutputReadLine();

        _event.WaitOne();
    }

    public void Down()
    => Processor.Kill(_options.Port);
}
