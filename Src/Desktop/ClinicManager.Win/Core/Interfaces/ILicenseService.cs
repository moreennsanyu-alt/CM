namespace ClinicManager.Core.Interfaces;

/// <summary>
/// Resolves which licensed features/modules are available for the current
/// clinic/tenant. Used by the Shell when building the module catalog.
/// </summary>
public interface ILicenseService
{
    bool HasFeature(string featureName);
    string LicenseTier { get; }
}
