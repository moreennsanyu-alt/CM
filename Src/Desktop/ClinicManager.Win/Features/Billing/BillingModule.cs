using ClinicManager.Core.Constants;
using Prism.Ioc;
using Prism.Modularity;

namespace ClinicManager.Billing;

public class BillingModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Example:
        // containerRegistry.RegisterForNavigation<BillingView, BillingViewModel>();
        // containerRegistry.Register<IBillingService, BillingService>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        // Example:
        // var regionManager = containerProvider.Resolve<IRegionManager>();
        // regionManager.RequestNavigate(RegionNames.MainRegion, nameof(BillingView));
    }
}
