using ClinicManager.Core.Events;
using FluentAssertions;
using NUnit.Framework;

namespace ClinicManager.Core.Tests.Events;

[TestFixture]
public class PatientSelectedEventTests
{
    [Test]
    public void Subscribers_Receive_Published_PatientId()
    {
        var sut = new PatientSelectedEvent();
        int? received = null;
        sut.Subscribe(patientId => received = patientId);

        sut.Publish(42);

        received.Should().Be(42);
    }
}
