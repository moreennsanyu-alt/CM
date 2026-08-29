namespace ClinicManager.Testing.Builders;

/// <summary>
/// Base class for fluent test-data builders (e.g. PatientBuilder,
/// AppointmentBuilder) used across module test projects. Concrete builders
/// live in their own module's test project, or here if genuinely shared
/// across modules (e.g. a shared Address or Money value object).
/// </summary>
public abstract class TestDataBuilder<TSelf, TResult> where TSelf : TestDataBuilder<TSelf, TResult>
{
    public abstract TResult Build();

    protected TSelf Self => (TSelf)this;
}
