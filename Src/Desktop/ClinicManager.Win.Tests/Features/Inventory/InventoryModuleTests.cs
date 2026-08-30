using ClinicManager.Win.Features.Inventory;
using ClinicManager.Testing.Fixtures;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Prism.Ioc;
using Prism.Modularity;

namespace ClinicManager.Inventory.Tests;

[TestFixture]
public class InventoryModuleTests : PrismTestBase
{
    [Test]
    public void Module_Implements_IModule()
    {
        var sut = new InventoryModule();

        sut.Should().BeAssignableTo<IModule>();
    }

    [Test]
    public void RegisterTypes_Does_Not_Throw()
    {
        var sut = new InventoryModule();
        var containerRegistryMock = new Mock<IContainerRegistry>();

        var act = () => sut.RegisterTypes(containerRegistryMock.Object);

        act.Should().NotThrow();
        // TODO: assert specific containerRegistryMock.Verify(...) calls once
        // RegisterTypes has concrete registrations.
    }
}
