using ClinicManager.Core.Constants;
using Prism.Ioc;
using Prism.Modularity;

namespace ClinicManager.Win.Features.Authentication;

public class AuthenticationModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Example:
        // containerRegistry.RegisterForNavigation<AuthenticationView, AuthenticationViewModel>();
        // containerRegistry.Register<IAuthenticationService, AuthenticationService>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        // Example:
        // var regionManager = containerProvider.Resolve<IRegionManager>();
        // regionManager.RequestNavigate(RegionNames.MainRegion, nameof(AuthenticationView));
    }
}
