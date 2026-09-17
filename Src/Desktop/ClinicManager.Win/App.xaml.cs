using System.Windows;
using ClinicManager.Win.Views;
using ClinicManager.Win.Features.Patients;
using ClinicManager.Win.Features.Scheduling;
using ClinicManager.Win.Features.Billing;
using ClinicManager.Win.Features.ClinicalRecords;
using ClinicManager.Win.Features.Pharmacy;
using ClinicManager.Win.Features.Inventory;
using ClinicManager.Win.Features.Staff;
using ClinicManager.Win.Features.Reception;
using ClinicManager.Win.Features.Reporting;
using ClinicManager.Win.Features.Notifications;
using ClinicManager.Win.Features.Scheduling;
using ClinicManager.Win.Features.Authentication;
using Prism.Ioc;
using Prism.Modularity;

namespace ClinicManager.Win;

public partial class App
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Register Shell-level / cross-cutting services here.
        // Example: containerRegistry.RegisterSingleton<ILicenseService, LicenseService>();
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        var moduleTypes = typeof(App).Assembly.GetTypes()
                .Where(t => typeof(IModule).IsAssignableFrom(t)
                            && t is { IsAbstract: false, IsInterface: false, IsPublic: true });

            foreach (var moduleType in moduleTypes)
            {
                moduleCatalog.AddModule(new ModuleInfo
                {
                    ModuleName = moduleType.Name,
                    ModuleType = moduleType.AssemblyQualifiedName,
                    InitializationMode = InitializationMode.WhenAvailable
                });
            }
    }
}
