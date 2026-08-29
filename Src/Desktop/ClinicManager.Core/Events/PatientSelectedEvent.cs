using Prism.Events;

namespace ClinicManager.Core.Events;

/// <summary>
/// Published when a patient is selected anywhere in the app (e.g. Patients module),
/// so other modules (Scheduling, Billing, ClinicalRecords, ...) can react without
/// referencing each other directly.
/// </summary>
public class PatientSelectedEvent : PubSubEvent<int>
{
}
