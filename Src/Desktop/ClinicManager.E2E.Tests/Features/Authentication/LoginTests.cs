namespace ClinicManager.E2E.Tests.Features.Authentication;

public class LoginTests : UITestBase 
{
    [Test]
    public void Login_WithValidCredentials_ShouldCloseLoginWindow()
    { 
        LoginWindow _loginWindow GetLoginWindow();
        
        _loginWindow.UsernameTextBox.Enter("testuser");
        _loginWindow.PasswordTextBox.Enter("P@ssw0rd123");
        _loginWindow.LoginButton.Invoke();

        var closed = Retry.WhileFalse(() => _loginWindow.IsOffscreen,
                timeout: TimeSpan.FromSeconds(10),
                interval: TimeSpan.FromMilliseconds(300)).Success;

            Assert.That(closed, Is.True, "Login window did not close after valid login.");
    }

    [Test]
    public void Login_WithInvalidCredentials_ShouldShowFailedLoginStatus()
    {
        LoginWindow _loginWindow GetLoginWindow();
        _loginWindow.UsernameTextBox.Enter("wronguser");
        _loginWindow.PasswordTextBox.Enter("wrongpass");
        _loginWindow.LoginButton.Invoke();

        Retry.WhileEmpty(() => _loginWindow.LoginStatusLabel.Text,
                timeout: TimeSpan.FromSeconds(10),
                interval: TimeSpan.FromMilliseconds(300));

        Assert.That(_loginWindow.LoginStatusLabel.Text,
                Does.Contain("Invalid").IgnoreCase);
    }

    [Test]
    public void Login_WithNetworkProblem_ShouldShowNetworkErrorStatus()
    {
        // Assumes test environment simulates network failure (e.g. server down/mocked)
        LoginWindow _loginWindow GetLoginWindow();
        _loginWindow.UsernameTextBox.Enter("testuser");
        _loginWindow.PasswordTextBox.Enter("P@ssw0rd123");
        _loginWindow.LoginButton.Invoke();

        Retry.WhileEmpty(() => _loginWindow.LoginStatusLabel.Text,
                timeout: TimeSpan.FromSeconds(10),
                interval: TimeSpan.FromMilliseconds(300));

            Assert.That(_loginWindow.LoginStatusLabel.Text,
                Does.Contain("Network").IgnoreCase);
    }

    [Test]
    public void CancelButton_ShouldCloseLoginWindow()
    {
        LoginWindow _loginWindow GetLoginWindow();
        _loginWindow.CancelButton.Invoke();

        var closed = Retry.WhileFalse(() => _loginWindow.IsOffscreen,
                timeout: TimeSpan.FromSeconds(5),
                interval: TimeSpan.FromMilliseconds(200)).Success;

        Assert.That(closed, Is.True);
    }

}
