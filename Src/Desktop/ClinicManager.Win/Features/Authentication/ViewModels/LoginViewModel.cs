using ClinicManager.Win.Features.Authentication.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace ClinicManager.Win.Features.Authentication.ViewModels;

[GenerateViewModel(ImplementISupportServices = true)]
public partial class LoginViewModel(Func<string, string, Task<string>> loginFunction) 
{
    [GenerateProperty]
    string username = "Admin";

    [GenerateProperty]
    string password = "123";

    [GenerateProperty]
    string errorMessage;

    public bool IsAuthSuccess { get; set; }

    Func<string, string, Task<string>> LoginFunction = loginFunction;

    ICurrentWindowService CurrentWindowService => GetRequiredService<ICurrentWindowService>();

    [GenerateCommand]
    async Task Login()
    {
        ErrorMessage = await LoginFunction(Username, Password);
        if (!string.IsNullOrEmpty(ErrorMessage))
            return;
        IsAuthSuccess = true;
        CurrentWindowService.Close();
    }

    [GenerateCommand]
    async Task Cancel()
    {
        IsAuthSuccess = false;
        CurrentWindowService.Close();
    }

    bool CanLogin() => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

}
