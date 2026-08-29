using Moq;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;

namespace ClinicManager.Testing.Fixtures;

/// <summary>
/// Common base class for ViewModel / module unit tests. Provides a mocked
/// IEventAggregator, IRegionManager and IContainerProvider so tests don't
/// each re-wire the same Prism plumbing.
/// </summary>
public abstract class PrismTestBase
{
    protected Mock<IEventAggregator> EventAggregatorMock { get; } = new();
    protected Mock<IRegionManager> RegionManagerMock { get; } = new();
    protected Mock<IContainerProvider> ContainerProviderMock { get; } = new();
}
