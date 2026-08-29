namespace ClinicManager.Core.Constants;

/// <summary>
/// Central list of Prism region names used by the Shell and by every module.
/// Modules should reference these constants instead of hardcoding region name strings.
/// </summary>
public static class RegionNames
{
    public const string MainRegion = "MainRegion";
    public const string MenuRegion = "MenuRegion";
    public const string StatusBarRegion = "StatusBarRegion";
    public const string NotificationRegion = "NotificationRegion";
}
