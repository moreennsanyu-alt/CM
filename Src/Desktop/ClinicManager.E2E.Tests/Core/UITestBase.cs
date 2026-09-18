using System;
using System.IO;
using System.Threading.Tasks;
using FlaUI.Core;
using FlaUI.Core.Capturing;
using FlaUI.Core.Logging;
using FlaUI.Core.Tools;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using FlaUI.TestUtilities;
using System.Runtime.InteropServices;
using System.Globalization;

namespace ClinicManager.E2E.Tests.Core
{
    /// <summary>
    /// Base class for ui tests with some helper methods.
    /// This class allows recording videos, taking screen shots on failed tests and
    /// starts and stops the application under test for each test or fixture.
    /// </summary>
    public abstract class UITestBase
    {
        private static readonly string TestResultsDirectory =
            Environment.GetEnvironmentVariable("NUNIT_RESULTS_DIRECTORY")
            ?? Path.GetFullPath(TestContext.CurrentContext.WorkDirectory);

        /// <summary>
        /// Member which holds the current video recorder.
        /// </summary>
        private VideoRecorder _recorder;

        /// <summary>
        /// The name of the current test method. Used for the video recorder.
        /// </summary>
        private string _testMethodName;

        protected AutomationBase Automation { get; private set; }
        protected Application Application { get; set; }

        protected virtual ApplicationStartMode ApplicationStartMode => ApplicationStartMode.OncePerTest;
        protected virtual bool KeepVideoForSuccessfulTests => false;
        protected virtual bool TakeScreenshots => true;
        protected virtual VideoRecordingMode VideoRecordingMode => VideoRecordingMode.OnePerTest;

        private static string _testDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");

        public string TestsMediaPath =>
            Path.Combine(
                TestResultsDirectory,
                "Media",
                SanitizeFileName(TestContext.CurrentContext.Test.Name),
                _testDateTime);

        public static string ApplicationPath = BuildInfo.ApplicationPath;

        static UITestBase()
        {
            NativeMethods.SetProcessDPIAware();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

            Mouse.MovePixelsPerMillisecond = 2;
            Retry.DefaultTimeout = TimeSpan.FromSeconds(5);
            Retry.DefaultInterval = TimeSpan.FromMilliseconds(250);
        }
