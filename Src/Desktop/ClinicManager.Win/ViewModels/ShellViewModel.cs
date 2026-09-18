using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ClinicManager.Win.Core.Events;

namespace ClinicManager.Win.ViewModels;

public sealed class ShellViewModel : BindableBase
{
    private readonly IEventAggregator _eventAggregator;

    public ShellViewModel(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
        LogoutCommand = new DelegateCommand(RequestLogout);
    }

    public DelegateCommand LogoutCommand { get; }

    private void RequestLogout()
    {
        _eventAggregator.GetEvent<LogoutRequestedEvent>()
            .Publish(new LogoutRequestedEventArgs());
    }
}
