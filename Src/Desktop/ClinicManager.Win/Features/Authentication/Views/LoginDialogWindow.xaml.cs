
namespace ClinicManager.Win.Features.Authentication.Views;

public partial class LoginDialogWindow : Window, IDialogWindow
{
    public LoginDialogWindow()
    {
        InitializeComponent();
    }

    public IDialogResult Result { get; set; }
}
