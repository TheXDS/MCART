# AGENTS.md — MCART

> Morgan's CLR Advanced Runtime: a general-purpose utility library for .NET.
> This file is the authoritative briefing for AI coding agents. Read it fully before planning any change.

---

## Project overview

MCART is a multi-package .NET library distributed on NuGet under the `TheXDS.MCART.*` namespace.
The codebase is in active development (alpha) and is currently being **translated to English** — all
new identifiers, XML doc comments, and user-facing strings must be written in English.

---

## Repository layout

```
src/
  MCART.slnx              ← root solution (cross-platform .slnx format)
  Directory.Build.props   ← shared MSBuild properties for ALL projects
  Directory.Build.targets ← shared MSBuild targets for ALL projects
  Targets/                ← granular MSBuild includes (packaging, versioning, compile options)
  Lib/                    ← platform-agnostic library projects
    MCART.Coloring/
    MCART.Mvvm/
    …
  Platform/               ← platform-targeted projects (WPF, Avalonia, WinForms, …)
Build/
  bin/                    ← compiled binaries
  Tests/                  ← dotnet test output
  Coverage/               ← ReportGenerator HTML reports
.github/workflows/        ← CI definitions (build, test, publish)
```

---

## Build

```powershell
dotnet build src/MCART.slnx
```

The solution uses the `.slnx` extension (cross-platform Visual Studio solution format). Always target
the solution file — do not build individual projects in isolation unless you have a scoped reason.

---

## Running tests

```powershell
# Run the full test suite (skip rebuild if already built)
dotnet test src/MCART.slnx --no-build

# Run tests for a single library
dotnet test src/Lib/MCART.Coloring.Tests/MCART.Coloring.Tests.csproj
```

Run tests locally before finalizing any change. Do not submit work that breaks existing tests.

---

## Code coverage

```powershell
dotnet test src/MCART.slnx --collect:"XPlat Code Coverage" --results-directory:./Build/Tests
reportgenerator -reports:./Build/Tests/*/coverage.cobertura.xml -targetdir:./Build/Coverage/
```

Coverage output lands in `Build/Coverage/`. Install `reportgenerator` globally if it is not present:
`dotnet tool install -g dotnet-reportgenerator-globaltool`.

---

## Packaging

```powershell
dotnet pack src/Lib/MCART.Coloring/MCART.Coloring.csproj --no-build
```

CI handles NuGet publishing via `.github/workflows/*`. Never bump version numbers manually —
versioning is controlled by `src/Targets/Package.targets` and `src/Directory.Build.props`.
Do not modify packaging targets unless the task explicitly requires it; changes there affect all packages.

---

## Centralized MSBuild settings

`src/Directory.Build.props` and `src/Directory.Build.targets` are inherited by every project.
Edits to these files — or to anything under `src/Targets/` — affect the **entire solution**.
Scope such changes carefully and document the intent clearly.

---

## Coding conventions

- **Language**: C#, targeting .NET 8.0 or later.
- **Identifiers and comments**: English only. If you encounter Spanish identifiers or doc comments
  while working in a file, translate them as part of the task.
- **XML documentation**: Required on all public members and types. Missing XML doc comments are
  treated as warnings by the build. Add `<summary>`, `<param>`, `<returns>`, and `<exception>` tags
  as appropriate.
- **Naming**: Projects and NuGet packages follow the `TheXDS.MCART.*` prefix pattern.
  New libraries go under `src/Lib/`; platform-specific variants go under `src/Platform/`.
- **Test framework**: Tests are written using *NUnit4* syntax with the `Assert.That` style. Mocks
  are provided by the *Moq* library.
- **Test pairing**: Every new public API must have a corresponding unit test in the matching
  `*.Tests` project. Create the test project if it does not already exist, following the naming
  pattern `MCART.<Module>.Tests`.
- **Scope**: Keep changes minimal and focused — one concern per commit or PR (single library,
  single fix, or single feature).

---

## License

Source files must carry a license header consistent with the existing files in the same project.
Check the nearest existing `.cs` file for the correct header before creating new files.

---

## What NOT to do

- Do not modify `src/Directory.Build.props`, `src/Directory.Build.targets`, or anything under
  `src/Targets/` unless the task explicitly targets build infrastructure.
- Do not commit without running `dotnet test src/MCART.slnx --no-build` and confirming it passes.
- Do not write new code, comments, or identifiers in Spanish.
- Do not bump version numbers or edit NuGet metadata by hand.
- Do not create new projects outside `src/Lib/` or `src/Platform/` without a clear structural reason.
- Do not install additional 3rd party dependencies on any project without an explicit reason to do so.