using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ClinicManager.E2E.Tests;

public sealed class TestSettings
{
    public string OutputDirectory { get; }
    public string FileExtension { get; }

    public TestSettings(string outputDirectory = null, string fileExtension = null)
    {
        OutputDirectory = string.IsNullOrWhiteSpace(outputDirectory)
            ? Path.Combine(AppContext.BaseDirectory, "TestVideos")
            : outputDirectory;

        FileExtension = string.IsNullOrWhiteSpace(fileExtension)
            ? ".mp4"
            : fileExtension;
    }

    public string CreateFilePath(System.Reflection.MethodInfo methodUnderTest)
    {
        if (methodUnderTest == null)
        {
            throw new ArgumentNullException(nameof(methodUnderTest));
        }

        Directory.CreateDirectory(OutputDirectory);

        string className = methodUnderTest.DeclaringType != null
            ? methodUnderTest.DeclaringType.Name
            : "UnknownTestClass";

        string methodName = methodUnderTest.Name;
        string safeName = Regex.Replace($"{className}_{methodName}", @"[^A-Za-z0-9_.-]", "_");
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");

        return Path.Combine(OutputDirectory, $"{safeName}_{timestamp}{FileExtension}");
    }
}
