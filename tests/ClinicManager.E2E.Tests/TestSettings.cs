using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ClinicManager.E2E.Tests;

public sealed class TestSettings
{
    public string OutputDirectory { get; }
    public string FileExtension { get; }

    public TestSettings(string? outputDirectory = null, string? fileExtension = null)
    {
        OutputDirectory = string.IsNullOrWhiteSpace(outputDirectory)
            ? Path.Combine(AppContext.BaseDirectory, "TestVideos")
            : outputDirectory;

        FileExtension = string.IsNullOrWhiteSpace(fileExtension)
            ? ".mp4"
            : fileExtension;
    }

    public string CreateFilePath(MethodInfo methodUnderTest)
    {
        if (methodUnderTest == null)
        {
            throw new ArgumentNullException(nameof(methodUnderTest));
        }

        Directory.CreateDirectory(OutputDirectory);

        var typeName = methodUnderTest.DeclaringType != null
            ? methodUnderTest.DeclaringType.Name
            : "UnknownTestClass";

        var methodName = methodUnderTest.Name;
        var safeName = Regex.Replace($"{typeName}_{methodName}", "[^A-Za-z0-9_.-]", "_");

        var stamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
        return Path.Combine(OutputDirectory, $"{safeName}_{stamp}{FileExtension}");
    }
}
