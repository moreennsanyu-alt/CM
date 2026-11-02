using System;
using System.Reflection;
using Xunit;
using Xunit.v3;

namespace ClinicManager.E2E.Tests;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class RecordTestAttribute : BeforeAfterTestAttribute
{
    private readonly TestSettings _settings;
    private TestRecorder? _recorder;

    public RecordTestAttribute()
        : this(new TestSettings())
    {
    }

    public RecordTestAttribute(string outputDirectory)
        : this(new TestSettings(outputDirectory))
    {
    }

    public RecordTestAttribute(TestSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    public override void Before(MethodInfo methodUnderTest, IXunitTest test)
    {
        string filePath = _settings.CreateFilePath(methodUnderTest);
        _recorder = new TestRecorder(filePath);
        _recorder.Start();
    }

    public override void After(MethodInfo methodUnderTest, IXunitTest test)
    {
        if (_recorder != null)
        {
            _recorder.Stop();
            _recorder.Dispose();
            _recorder = null;
        }
    }
}
