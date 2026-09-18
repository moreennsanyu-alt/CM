namespace ClinicManager.Win.Features.Authentication.Services;

public enum LoginResult
{
    Success,
    InvalidCredentials,
    NetworkError
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
