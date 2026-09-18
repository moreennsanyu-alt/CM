using System.Windows;
using Prism.Services.Dialogs;

namespace ClinicManager.Win.Features.Authentication.Views;

public partial class LoginDialogWindow : Window, IDialogWindow
{
    public LoginDialogWindow() => InitializeComponent();

    public IDialogResult Result { get; set; } = new DialogResult(ButtonResult.None);
}
