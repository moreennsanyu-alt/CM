using Prism.Events;

namespace ClinicManager.Core.Events;

/// <summary>
/// Published by Scheduling when an appointment is marked complete.
/// Billing and ClinicalRecords subscribe to this instead of taking a project
/// reference on the Scheduling module.
/// </summary>
public class AppointmentCompletedEvent : PubSubEvent<int>
{
}
