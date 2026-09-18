using ClinicManager.Win.Features.Authentication.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace ClinicManager.Win.Features.Authentication.ViewModels;

public sealed class LoginViewModel : BindableBase, IDialogAware
{
    private readonly IAuthenticationService _authenticationService;

    [ObservableProperty]
    private string _username = string.Empty;
    
    [ObservableProperty]
    private string _password = string.Empty;
     
    [ObservableProperty]
    private string _statusMessage = string.Empty;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private bool _isBusy;

    public LoginViewModel(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        CancelCommand = new DelegateCommand(Cancel);
    }

    

    public DelegateCommand CancelCommand { get; }

    [ObservableProperty]
    public string title => "Sign in";
    
    public event Action<IDialogResult>? RequestClose;

    public bool CanCloseDialog() => !IsBusy;
    public void OnDialogOpened(IDialogParameters parameters) { }
    public void OnDialogClosed() { }

    [RelayCommand(CanExecute = nameof(CanLogin))]
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

    private bool CanLogin() => !IsBusy

    private void Cancel() => RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
}
