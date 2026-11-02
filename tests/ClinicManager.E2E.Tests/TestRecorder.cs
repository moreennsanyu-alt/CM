using System;
using System.IO;
using System.Threading;
using ScreenRecorderLib;

namespace ClinicManager.E2E.Tests;

public sealed class TestRecorder : IDisposable
{
    private static readonly TimeSpan StopTimeout = TimeSpan.FromSeconds(30);

    private readonly Recorder _recorder;
    private readonly ManualResetEventSlim _finished = new(false);
    private string? _error;
    private bool _started;
    private bool _disposed;

    public string OutputFilePath { get; }

    public TestRecorder(string outputFilePath)
    {
        if (string.IsNullOrWhiteSpace(outputFilePath))
        {
            throw new ArgumentException("A valid output file path is required.", nameof(outputFilePath));
        }

        OutputFilePath = outputFilePath;

        string? outputDirectory = Path.GetDirectoryName(OutputFilePath);
        if (!string.IsNullOrEmpty(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        var options = new RecorderOptions
        {
            OutputOptions = new OutputOptions
            {
                RecorderMode = RecorderMode.Video
            },
            VideoEncoderOptions = new VideoEncoderOptions
            {
                Framerate = 30,
                IsFixedFramerate = true,
                Encoder = new H264VideoEncoder
                {
                    BitrateMode = H264BitrateControlMode.Quality,
                    EncoderProfile = H264Profile.Main
                }
            }
        };

        _recorder = Recorder.CreateRecorder(options);
        _recorder.OnRecordingComplete += (_, _) => _finished.Set();
        _recorder.OnRecordingFailed += (_, e) =>
        {
            _error = e.Error;
            _finished.Set();
        };
    }

    public void Start()
    {
        _finished.Reset();
        _error = null;
        _recorder.Record(OutputFilePath);
        _started = true;
    }

    public void Stop()
    {
        if (!_started)
        {
            return;
        }

        _started = false;

        if (_recorder.Status == RecorderStatus.Recording ||
            _recorder.Status == RecorderStatus.Paused)
        {
            _recorder.Stop();
        }

        // Stop() returns before the file is finalized.
        if (!_finished.Wait(StopTimeout))
        {
            throw new TimeoutException($"Recording did not finish within {StopTimeout.TotalSeconds}s: {OutputFilePath}");
        }

        if (_error != null)
        {
            throw new InvalidOperationException($"Screen recording failed: {_error}");
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        try
        {
            Stop();
        }
        catch
        {
            // Never let recorder cleanup mask a test result.
        }

        _recorder.Dispose();
        _finished.Dispose();
    }
}
