# Repository Instructions

## Project overview

This repository contains a Windows-only hospital management desktop application built with .NET 10, WPF, and Entity Framework Core.

- `HospitalManagement.App/` is the WPF presentation project. It contains XAML views, view models, commands, styles, application startup, and dependency-injection registration.
- `HospitalManagement.Data/` is the data-access project. It contains EF Core entities, `HospitalDbContext`, data-manager interfaces and implementations, and migrations.
- `HospitalManagement.sln` is the solution entry point.
- The app uses the local SQL Server Express instance `.\SQLEXPRESS` with the `HospitalDBContext` connection string in `HospitalManagement.App/App.config`.

## Build and run

Run commands from the repository root.

```powershell
dotnet restore HospitalManagement.sln
dotnet build HospitalManagement.sln
dotnet run --project HospitalManagement.App/HospitalManagement.App.csproj
```

The WPF application requires Windows. Database-backed screens also require SQL Server Express and an initialized `HospitalDB` database.

There is currently no automated test project. For every change, at minimum run:

```powershell
dotnet build HospitalManagement.sln
```

For UI or data-flow changes, also launch the application and manually exercise the affected screen and command.

## Architecture and conventions

- Keep UI layout and bindings in `Views/*.xaml`; keep code-behind limited to view initialization, dependency resolution, and window-specific behavior.
- Put bindable state and UI actions in `ViewModels/`. View models inherit `BaseViewModel`, raise `PropertyChanged`, and expose actions through `ICommand`/`RelayCommand`.
- Put persistence logic in `HospitalManagement.Data/DataManagers/`, behind the matching interface. Do not query `HospitalDbContext` directly from a view or view model.
- Put EF Core entities and related enums in `HospitalManagement.Data/DataModels/`.
- Register new views, view models, and data-manager services in `HospitalManagement.App/App.xaml.cs`.
- Use async EF Core APIs for database I/O and propagate `Task` where practical. Avoid introducing new `async void` methods except event-handler or `ICommand` boundaries required by WPF.
- Preserve nullable-reference-type annotations. New non-nullable properties should be initialized or made required by construction; optional database fields should be explicitly nullable.
- Follow the existing C# naming style: PascalCase for types, methods, and public properties; `_camelCase` for private fields; interfaces prefixed with `I`.

## Entity Framework Core changes

Treat the entity classes, relationship configuration, migrations, and model snapshot as one coherent unit.

Create a migration from the repository root with:

```powershell
dotnet ef migrations add <MigrationName> --project HospitalManagement.Data/HospitalManagement.Data.csproj --startup-project HospitalManagement.App/HospitalManagement.App.csproj
```

Apply migrations with:

```powershell
dotnet ef database update --project HospitalManagement.Data/HospitalManagement.Data.csproj --startup-project HospitalManagement.App/HospitalManagement.App.csproj
```

Do not hand-edit generated migration designer files or `HospitalDbContextModelSnapshot.cs` unless repairing a known EF tooling issue. Do not change the database connection string or database schema as an incidental part of unrelated work.

## Change guidelines

- Make focused changes and preserve the existing separation between presentation and data access.
- Reuse dependency injection rather than constructing data managers or `HospitalDbContext` manually.
- When adding a screen, update its XAML, view model, DI registration, and navigation/tab wiring together as applicable.
- When changing a binding, command, or property name, search all XAML and C# references so the runtime binding is not silently broken.
- Keep generated output (`bin/`, `obj/`, and user-specific `*.csproj.user` files) out of source changes.
- Do not add or modify migrations for model changes unless the requested work includes the corresponding schema change.
- Never commit real patient data, credentials, database files, or machine-specific connection details.

## Verification checklist

Before finishing a change:

1. Build the complete solution and resolve new warnings or errors caused by the change.
2. If XAML changed, open the affected view and check bindings, commands, layout, and modal-window behavior.
3. If persistence changed, verify create/read/update/delete behavior and required navigation properties against a disposable local database.
4. If the EF model changed, inspect the generated migration and confirm the solution still builds.
5. Review `git diff` and exclude generated, user-specific, database, or unrelated files.

## Feature completion review

After implementing a roadmap feature and completing its normal verification, read and follow `skills/feature-completion-review/SKILL.md` before declaring the feature complete.

- Use the skill to assess acceptance criteria, architecture, healthcare-data safety, verification evidence, documentation, and roadmap status.
- Treat its completion verdict as a release gate for the feature.
- If it recommends creating or updating a skill, present the proposal to the user first. Do not modify skills without explicit approval.
- A review does not authorize unrelated fixes or broader refactoring; obtain separate direction before expanding scope.
