using ClinicManager.Core.Constants;
using Prism.Ioc;
using Prism.Modularity;

namespace ClinicManager.Win.Features.ClinicalRecords;

public class ClinicalRecordsModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Example:
        // containerRegistry.RegisterForNavigation<ClinicalRecordsView, ClinicalRecordsViewModel>();
        // containerRegistry.Register<IClinicalRecordsService, ClinicalRecordsService>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        // Example:
        // var regionManager = containerProvider.Resolve<IRegionManager>();
        // regionManager.RequestNavigate(RegionNames.MainRegion, nameof(ClinicalRecordsView));
    }
}
