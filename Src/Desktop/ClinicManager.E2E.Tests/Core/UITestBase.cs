using FlaUI.Core;
using FlaUI.Core.Capturing;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.UIA3;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ClinicManager.E2E.Tests.Core;

public  class UITestBase : FlaUITestBase
{
public static string ApplicationPath;
    static UITestBase()
    {
        NativeMethods.SetProcessDPIAware();
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

        Mouse.MovePixelsPerMillisecond = 2;
        Retry.DefaultTimeout = TimeSpan.FromSeconds(5);
        Retry.DefaultInterval = TimeSpan.FromMilliseconds(250);
    }
    protected override AutomationBase GetAutomation()
    {
        return new UIA3Automation();
    }

    protected override FlaUI.Core.Application StartApplication()
    { 
        var startInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = ApplicationPath,
            WorkingDirectory = System.IO.Path.GetDirectoryName(ApplicationPath),
            UseShellExecute = false
        };

        return FlaUI.Core.Application.Launch(startInfo);
  }  


    private static class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetProcessDPIprotected override AutomationBase GetAutomation()
        {
            return new UIA3Automation();
        }
        Aware();
    }
}
