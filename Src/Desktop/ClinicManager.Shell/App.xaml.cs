using System.Windows;
using ClinicManager.Shell.Views;
using ClinicManager.Patients;
using ClinicManager.Scheduling;
using ClinicManager.Billing;
using ClinicManager.ClinicalRecords;
using ClinicManager.Pharmacy;
using ClinicManager.Inventory;
using ClinicManager.Staff;
using ClinicManager.Reception;
using ClinicManager.Reporting;
using ClinicManager.Notifications;
using ClinicManager.Authentication;
using Prism.Ioc;
using Prism.Modularity;

namespace ClinicManager.Shell;

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
        // TODO: gate optional modules behind ILicenseService.HasFeature(...) once
        // the license service is implemented. All modules are registered
        // unconditionally for now so the solution builds and runs end to end.

        // Patients
        moduleCatalog.AddModule(new ModuleInfo
        {
            ModuleName = nameof(PatientsModule),
            ModuleType = typeof(PatientsModule).AssemblyQualifiedName,
            InitializationMode = InitializationMode.WhenAvailable
        });

        // Scheduling
        moduleCatalog.AddModule(new ModuleInfo
        {
            ModuleName = nameof(SchedulingModule),
            ModuleType = typeof(SchedulingModule).AssemblyQualifiedName,
            InitializationMode = InitializationMode.WhenAvailable
        });

        // Billing
        moduleCatalog.AddModule(new ModuleInfo
        {
            ModuleName = nameof(BillingModule),
            ModuleType = typeof(BillingModule).AssemblyQualifiedName,
            InitializationMode = InitializationMode.WhenAvailable
        });

        // ClinicalRecords
        moduleCatalog.AddModule(new ModuleInfo
        {
            ModuleName = nameof(ClinicalRecordsModule),
            ModuleType = typeof(ClinicalRecordsModule).AssemblyQualifiedName,
            InitializationMode = InitializationMode.WhenAvailable
        });

        // Pharmacy
        moduleCatalog.AddModule(new ModuleInfo
        {
            ModuleName = nameof(PharmacyModule),
            ModuleType = typeof(PharmacyModule).AssemblyQualifiedName,
            InitializationMode = InitializationMode.WhenAvailable
        });

        // Inventory
        moduleCatalog.AddModule(new ModuleInfo
        {
            ModuleName = nameof(InventoryModule),
            ModuleType = typeof(InventoryModule).AssemblyQualifiedName,
            InitializationMode = InitializationMode.WhenAvailable
        });

        // Staff
        moduleCatalog.AddModule(new ModuleInfo
        {
            ModuleName = nameof(StaffModule),
            ModuleType = typeof(StaffModule).AssemblyQualifiedName,
            InitializationMode = InitializationMode.WhenAvailable
        });

        // Reception
        moduleCatalog.AddModule(new ModuleInfo
        {
            ModuleName = nameof(ReceptionModule),
            ModuleType = typeof(ReceptionModule).AssemblyQualifiedName,
            InitializationMode = InitializationMode.WhenAvailable
        });

        // Reporting
        moduleCatalog.AddModule(new ModuleInfo
        {
            ModuleName = nameof(ReportingModule),
            ModuleType = typeof(ReportingModule).AssemblyQualifiedName,
            InitializationMode = InitializationMode.WhenAvailable
        });

        // Notifications
        moduleCatalog.AddModule(new ModuleInfo
        {
            ModuleName = nameof(NotificationsModule),
            ModuleType = typeof(NotificationsModule).AssemblyQualifiedName,
            InitializationMode = InitializationMode.WhenAvailable
        });

        // Authentication
        moduleCatalog.AddModule(new ModuleInfo
        {
            ModuleName = nameof(AuthenticationModule),
            ModuleType = typeof(AuthenticationModule).AssemblyQualifiedName,
            InitializationMode = InitializationMode.WhenAvailable
        });
    }
}
