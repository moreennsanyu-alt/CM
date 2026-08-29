using Moq;
using Prism.Regions;

namespace ClinicManager.Testing.Mocks;

/// <summary>
/// Helpers for building a mocked IRegionManager that satisfies common module
/// initialization/navigation assertions without a real Prism region host.
/// </summary>
public static class MockRegionManagerFactory
{
    public static Mock<IRegionManager> Create()
    {
        var regionManagerMock = new Mock<IRegionManager>();
        var regionCollectionMock = new Mock<IRegionCollection>();

        regionManagerMock
            .Setup(rm => rm.Regions)
            .Returns(regionCollectionMock.Object);

        return regionManagerMock;
    }
}
