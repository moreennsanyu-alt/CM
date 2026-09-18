using ClinicManager.Win.ViewModels;
using ClinicManager.Win.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace ClinicManager.Win;

public partial class App
{
    protected override Window CreateShell() => null;

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<Shell>();
        containerRegistry.Register<ShellViewModel>();
    }
    
    private Shell? _shell;

    protected override Window CreateShell()
    {
        _shell = Container.Resolve<Shell>();
        return _shell;
    }
    
    protected override void InitializeShell()
    {
        if (_shell is null)
            throw new InvalidOperationException("The shell has not been created.");

        Current.MainWindow = _shell;
        ShowLoginWindow();
}
    void ShowLoginWindow()
    {
        LoginViewModel loginViewModel = new LoginViewModel(LoginAction);
        LoginWindow loginWindow = new LoginWindow() { DataContext = loginViewModel };
        loginWindow.ShowDialog();
        if (!loginViewModel.IsAuthSuccess) {
            Shutdown();
        }
    }

    void OnLogoutMessage()
    {
        var authService = Container.Resolve<IAuthenticationService>();
        authService.Logout();
        ShowLoginWindow();
    }

    async Task<string> LoginAction(string login, string password) 
    {
        var authService = Container.Resolve<IAuthenticationService>();
        LoginResult result = await authService.LoginAsync(login, password);
        if (result.IsSuccess()) {
            _shell.Show();
            _shell.Activate();
        }

        return result.ToDisplayMessage();

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
