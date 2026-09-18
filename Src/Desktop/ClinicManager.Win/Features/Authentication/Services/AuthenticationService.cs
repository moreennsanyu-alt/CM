namespace ClinicManager.Win.Features.Authentication.Services;

public enum LoginResult
{
    Success,
    InvalidCredentials,
    AccountLocked,
    AccountDisabled,
    AccountExpired,
    PasswordExpired,
    EmailNotVerified,
    TooManyAttempts,
    NetworkError,
    ServerError,
    Timeout,
    InvalidLicense,
    LicenseExpired,
    MaintenanceMode,
    UnsupportedClientVersion,
    Cancelled,
    Unknown
}
public static class LoginResultExtensions
{
    public static string ToDisplayMessage(this LoginResult result) => result switch
    {
        LoginResult.Success => "",
        LoginResult.InvalidCredentials => "Invalid username or password.",
        LoginResult.AccountLocked => "Your account is locked. Please contact support.",
        LoginResult.AccountDisabled => "Your account has been disabled.",
        LoginResult.AccountExpired => "Your account has expired.",
        LoginResult.PasswordExpired => "Your password has expired. Please reset it.",
        LoginResult.EmailNotVerified => "Please verify your email address before logging in.",
        LoginResult.TooManyAttempts => "Too many failed attempts. Please try again later.",
        LoginResult.NetworkError => "Unable to connect. Please check your internet connection.",
        LoginResult.ServerError => "A server error occurred. Please try again later.",
        LoginResult.Timeout => "The request timed out. Please try again.",
        LoginResult.InvalidLicense => "Invalid license. Please contact support.",
        LoginResult.LicenseExpired => "Your license has expired. Please renew to continue.",
        LoginResult.MaintenanceMode => "The service is currently under maintenance. Please try again later.",
        LoginResult.UnsupportedClientVersion => "Your application version is outdated. Please update to continue.",
        LoginResult.Cancelled => "Login was cancelled.",
        LoginResult.Unknown => "An unexpected error occurred.",
        _ => "An unexpected error occurred."
    };

    public static bool IsSuccess(this LoginResult result) => result == LoginResult.Success;

    public static bool IsRetryable(this LoginResult result) => result switch
    {
        LoginResult.NetworkError => true,
        LoginResult.ServerError => true,
        LoginResult.Timeout => true,
        LoginResult.MaintenanceMode => true,
        _ => false
    };
}
public interface IAuthenticationService
{
    bool IsLoggedIn { get; }
    Task<LoginResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default);
    void Logout();
}

public sealed class AlwaysSuccessfulAuthenticationService : IAuthenticationService
{
    public bool IsLoggedIn { get; private set; }

    public Task<LoginResult> LoginAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        IsLoggedIn = true;
        return Task.FromResult(LoginResult.Success);
    }

    public void Logout() => IsLoggedIn = false;
}
