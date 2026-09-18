
namespace ClinicManager.Win.Features.Authentication.Views;

public partial class LoginDialogWindow : Window, IDialogWindow
{
    public LoginDialogWindow()
    {
        InitializeComponent();
    }

    public IDialogResult Result { get; set; }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
            DragMove();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
