using ClinicManager.Core.Constants;
using Prism.Ioc;
using Prism.Modularity;

namespace ClinicManager.Patients;

public class PatientsModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Example:
        // containerRegistry.RegisterForNavigation<PatientsView, PatientsViewModel>();
        // containerRegistry.Register<IPatientsService, PatientsService>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        // Example:
        // var regionManager = containerProvider.Resolve<IRegionManager>();
        // regionManager.RequestNavigate(RegionNames.MainRegion, nameof(PatientsView));
    }
}
