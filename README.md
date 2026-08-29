# ClinicManager

WPF clinic management system built with Prism, organized by feature. Targets
**.NET 10**.

## Solution layout

- **ClinicManager.Core** - Prism service interfaces, cross-module events, shared
  non-UI infrastructure (constants, enums, models). No WPF/UI dependency.
- **ClinicManager.Presentation** - shared WPF resources: controls, behaviors,
  converters, styles, themes. Referenced by the Shell and every module.
- **ClinicManager.Shell** - startup WPF application. Hosts the main window /
  regions, bootstraps Prism, and registers all modules.
- **src/Modules/ClinicManager.{Module}** - one project per feature module
  (Patients, Scheduling, Billing, ClinicalRecords, Pharmacy, Inventory, Staff,
  Reception, Reporting, Notifications, Authentication). Each module owns its
  own Views / ViewModels / Models / Services and communicates with other
  modules only through `ClinicManager.Core` interfaces and events
  (`IEventAggregator`), never via direct project references between modules.

## Reference direction

```
Shell -> all modules, Presentation, Core
Modules -> Presentation, Core   (never another module)
Presentation -> Core
Core -> (nothing)
```

## Central Package Management

All NuGet package versions are pinned once in `Directory.Packages.props` at
the solution root (`ManagePackageVersionsCentrally=true`). Every `csproj`
uses bare `<PackageReference Include="..." />` with no `Version` attribute.
To bump a version, edit `Directory.Packages.props` only.

`Directory.Build.props` (root) sets solution-wide defaults (`Nullable`,
`ImplicitUsings`, `LangVersion`). `test/Directory.Build.props` layers
test-only defaults (see below) on top of it for every project under `test/`.

## Tests

- **ClinicManager.Testing** (`test/ClinicManager.Testing`) - shared testing
  infrastructure: Prism test base classes, fluent test-data builders, mock
  factories. This is a plain class library, not a test project itself - it
  explicitly opts out of the `test/Directory.Build.props` test defaults
  (`IsTestProject=false`, `OutputType=Library`) since it has no tests of its
  own and is only referenced by the real test projects.
- **ClinicManager.Core.Tests**, **ClinicManager.Presentation.Tests**,
  **ClinicManager.Shell.Tests** - one test project per top-level project.
- **test/Modules/ClinicManager.{Module}.Tests** - one test project per
  feature module, mirroring `src/Modules`. Each ships a starter
  `{Module}ModuleTests.cs` that checks the module implements `IModule` and
  that `RegisterTypes` doesn't throw.

### Test stack

- **NUnit**, running on **Microsoft.Testing.Platform (MTP)** rather than
  VSTest - enabled via `EnableNUnitRunner` + `OutputType=Exe` +
  `TestingPlatformDotnetTestSupport` in `test/Directory.Build.props`, so
  every test project builds to a runnable executable
  (`dotnet run --project test/Modules/ClinicManager.Patients.Tests`) and
  also works with `dotnet test` / Test Explorer.
- **Microsoft.Testing.Extensions.CodeCoverage** for code coverage
  (coverlet.collector is not used - it isn't compatible with the NUnit MTP
  runner). Run coverage with:
  ```
  dotnet test --coverage
  ```
- **Moq** + **FluentAssertions** for mocking/assertions, **Bogus** (in
  `ClinicManager.Testing`) for generating fake test data.

Each test project references its corresponding source project plus
`ClinicManager.Testing`, never another module's test project.

## Licensing / dynamic module loading

`App.xaml.cs` (`ConfigureModuleCatalog`) is the seam where modules will be
gated behind `ILicenseService.HasFeature(...)` (see
`ClinicManager.Core/Interfaces/ILicenseService.cs`). All modules are
registered unconditionally for now.
