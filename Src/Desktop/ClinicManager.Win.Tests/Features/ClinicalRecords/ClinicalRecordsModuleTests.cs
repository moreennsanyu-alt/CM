using ClinicManager.Win.Features.ClinicalRecords;
using ClinicManager.Testing.Fixtures;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Prism.Ioc;
using Prism.Modularity;

namespace ClinicManager.ClinicalRecords.Tests;

[TestFixture]
public class ClinicalRecordsModuleTests : PrismTestBase
{
    [Test]
    public void Module_Implements_IModule()
    {
        var sut = new ClinicalRecordsModule();

        sut.Should().BeAssignableTo<IModule>();
    }

    [Test]
    public void RegisterTypes_Does_Not_Throw()
    {
        var sut = new ClinicalRecordsModule();
        var containerRegistryMock = new Mock<IContainerRegistry>();

        var act = () => sut.RegisterTypes(containerRegistryMock.Object);

        act.Should().NotThrow();
        // TODO: assert specific containerRegistryMock.Verify(...) calls once
        // RegisterTypes has concrete registrations.
    }
}
