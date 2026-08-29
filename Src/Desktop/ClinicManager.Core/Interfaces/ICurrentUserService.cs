namespace ClinicManager.Core.Interfaces;

/// <summary>
/// Provides access to the currently logged-in staff member and their roles.
/// </summary>
public interface ICurrentUserService
{
    int? UserId { get; }
    string? DisplayName { get; }
    IReadOnlyCollection<string> Roles { get; }
    bool IsInRole(string role);
}
