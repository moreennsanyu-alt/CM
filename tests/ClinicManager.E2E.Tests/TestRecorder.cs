using System;
using System.IO;
using ScreenRecorderLib;

namespace ClinicManager.E2E.Tests;

public sealed class TestRecorder : IDisposable
{
    private readonly Recorder _recorder;

    public string OutputFilePath { get; }

    public TestRecorder(string outputFilePath)
    {
        if (string.IsNullOrWhiteSpace(outputFilePath))
        {
            throw new ArgumentException("A valid output file path is required.", nameof(outputFilePath));
        }

        OutputFilePath = outputFilePath;

        var outputDirectory = Path.GetDirectoryName(OutputFilePath);
        if (!string.IsNullOrEmpty(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        var options = new RecorderOptions
        {
            RecorderMode = RecorderMode.Video,
            OutputOptions = new OutputOptions
            {
                FileName = OutputFilePath
            }
        };

        _recorder = Recorder.CreateRecorder(options);
    }

    public void Start()
    {
        _recorder.Record();
    }

    public void Stop()
    {
        _recorder.Stop();
    }

    public void Dispose()
    {
        try
        {
            _recorder.Stop();
        }
        catch
        {
            // Ignore disposal-time shutdown issues.
        }

        _recorder.Dispose();
    }
}
