using ClinicManager.Win.Features.Authentication.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace ClinicManager.Win.Features.Authentication.ViewModels;

public sealed class LoginViewModel : BindableBase, IDialogAware
{
    private readonly IAuthenticationService _authenticationService;
    private string _username = string.Empty;
    private string _password = string.Empty;
    private string _statusMessage = string.Empty;
    private bool _isBusy;

    public LoginViewModel(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        LoginCommand = new DelegateCommand(async () => await LoginAsync(), () => !IsBusy)
            .ObservesProperty(() => IsBusy);
        CancelCommand = new DelegateCommand(Cancel);
    }

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        private set => SetProperty(ref _isBusy, value);
    }

    public DelegateCommand LoginCommand { get; }
    public DelegateCommand CancelCommand { get; }

    public string Title => "Sign in";
    public event Action<IDialogResult>? RequestClose;

    public bool CanCloseDialog() => !IsBusy;
    public void OnDialogOpened(IDialogParameters parameters) { }
    public void OnDialogClosed() { }

    private async Task LoginAsync()
    {
        IsBusy = true;
        StatusMessage = string.Empty;

        try
        {
            var result = await _authenticationService.LoginAsync(Username, Password);
            switch (result)
            {
                case LoginResult.Success:
                    RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
                    break;
                case LoginResult.InvalidCredentials:
                    StatusMessage = "Invalid username or password.";
                    break;
                case LoginResult.NetworkError:
                    StatusMessage = "A network error occurred. Please try again.";
                    break;
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void Cancel() => RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
}
