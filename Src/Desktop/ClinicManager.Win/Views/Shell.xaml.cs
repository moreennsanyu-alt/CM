using System.Windows;
using ClinicManager.Win.Features.Authentication.Services;
using ClinicManager.Win.Features.Authentication.Views;
using Prism.Services.Dialogs;

namespace ClinicManager.Win.Views;

public partial class Shell : Window
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IDialogService _dialogService;

    public Shell(IAuthenticationService authenticationService, IDialogService dialogService)
    {
        _authenticationService = authenticationService;
        _dialogService = dialogService;
        InitializeComponent();
    }

    private void Logout_OnClick(object sender, RoutedEventArgs e)
    {
        _authenticationService.Logout();
        IsEnabled = false;

        _dialogService.ShowDialog("LoginDialog", new DialogParameters(), result =>
        {
            if (result.Result == ButtonResult.OK)
            {
                IsEnabled = true;
                Activate();
            }
            else
            {
                Application.Current.Shutdown();
            }
        });
    }
}
