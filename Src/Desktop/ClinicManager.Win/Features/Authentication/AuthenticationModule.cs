using ClinicManager.Win.Core.Events;
using ClinicManager.Win.Features.Authentication.Services;
using ClinicManager.Win.Features.Authentication.ViewModels;
using ClinicManager.Win.Features.Authentication.Views;
using Prism.Events;
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
        var events = containerProvider.Resolve<IEventAggregator>();
        events.GetEvent<LogoutRequestedEvent>().Subscribe(
            args => HandleLogoutRequested(args, containerProvider),
            ThreadOption.UIThread);

        ShowLoginIfRequired(containerProvider);
    }

    private static void HandleLogoutRequested(
        LogoutRequestedEventArgs args,
        IContainerProvider containerProvider)
    {
        if (args.Cancel)
            return;

        var authenticationService = containerProvider.Resolve<IAuthenticationService>();
        var dialogService = containerProvider.Resolve<IDialogService>();
        var shell = System.Windows.Application.Current?.MainWindow;

        authenticationService.Logout();
        if (shell is not null)
            shell.IsEnabled = false;

        dialogService.ShowDialog("LoginDialog", new DialogParameters(), result =>
        {
            if (result.Result == ButtonResult.OK)
            {
                if (shell is not null)
                {
                    shell.IsEnabled = true;
                    shell.Activate();
                }
            }
            else
            {
                System.Windows.Application.Current?.Shutdown();
            }
        });
    }

    private static void ShowLoginIfRequired(IContainerProvider containerProvider)
    {
        var authenticationService = containerProvider.Resolve<IAuthenticationService>();
        if (authenticationService.IsLoggedIn)
            return;

        var dialogService = containerProvider.Resolve<IDialogService>();
        var shell = System.Windows.Application.Current?.MainWindow;
        if (shell is not null)
            shell.IsEnabled = false;

        dialogService.ShowDialog("LoginDialog", new DialogParameters(), result =>
        {
            if (result.Result == ButtonResult.OK)
            {
                if (shell is not null)
                    shell.IsEnabled = true;
            }
            else
            {
                System.Windows.Application.Current?.Shutdown();
            }
        });
    }
}
