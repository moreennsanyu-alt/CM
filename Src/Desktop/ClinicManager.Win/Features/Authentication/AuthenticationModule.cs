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
       // containerRegistry.RegisterDialogWindow<LoginDialogWindow>();
        //containerRegistry.RegisterDialog<LoginView, LoginViewModel>("LoginDialog");
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        
    }

    
}
