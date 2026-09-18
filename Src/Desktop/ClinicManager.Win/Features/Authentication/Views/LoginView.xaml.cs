using System.Windows;
using System.Windows.Controls;
using ClinicManager.Win.Features.Authentication.ViewModels;

namespace ClinicManager.Win.Features.Authentication.Views;

public partial class LoginView : UserControl
{
    public LoginView() => InitializeComponent();

    private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is LoginViewModel viewModel)
            viewModel.Password = ((PasswordBox)sender).Password;
    }
}
