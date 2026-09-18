using ClinicManager.Win.Features.Authentication.Services;
using ClinicManager.Win.Features.Authentication.ViewModels;
using ClinicManager.Win.Features.Authentication.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Services.Dialogs;

namespace ClinicManager.Win.Features.Authentication;

public sealed class AuthenticationModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<IAuthenticationService, AlwaysSuccessfulAuthenticationService>();
        containerRegistry.RegisterDialogWindow<LoginDialogWindow>();
        containerRegistry.RegisterDialog<LoginView, LoginViewModel>("LoginDialog");
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        var authenticationService = containerProvider.Resolve<IAuthenticationService>();
        if (authenticationService.IsLoggedIn)
            return;

        var dialogService = containerProvider.Resolve<IDialogService>();
        var shell = System.Windows.Application.Current?.MainWindow;

        dialogService.ShowDialog("LoginDialog", new DialogParameters(), result =>
        {
            if (result.Result != ButtonResult.OK)
            {
                System.Windows.Application.Current?.Shutdown();
                return;
            }

            if (shell is not null)
                shell.IsEnabled = true;
        });

        if (shell is not null)
            shell.IsEnabled = false;
    }
}
