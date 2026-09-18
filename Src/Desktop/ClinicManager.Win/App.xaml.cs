using System.Windows;
using ClinicManager.Win.Features.Authentication.Views;
using ClinicManager.Win.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace ClinicManager.Win;

public partial class App
{
    protected override Window CreateShell() => Container.Resolve<Shell>();

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<Shell>();
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        foreach (var moduleType in typeof(App).Assembly.GetTypes()
                     .Where(t => typeof(IModule).IsAssignableFrom(t)
                              && t is { IsAbstract: false, IsInterface: false, IsPublic: true }))
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
