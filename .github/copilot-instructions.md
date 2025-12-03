<!-- Auto-generated guidance for AI coding agents working on MCART -->
# MCART — Copilot / Agent Instructions

This short guide highlights project structure, build/test workflows, and conventions that help an AI agent be immediately productive in this repository.

- **Root solution & build**: The main solution lives under `./src/` (see `src/MCART.slnx`). Use `dotnet build ./src/MCART.slnx` to build the whole repo. To run tests: `dotnet test ./src/MCART.slnx`.

- **Where code lives**: Libraries are under `src/Lib/` (examples: `src/Lib/MCART.Coloring`, `src/Lib/MCART.Mvvm`, `src/Lib/MCART.Windows`). Look for projects named `MCART.*` — follow the folder name for the csproj path.

- **Centralized MSBuild settings**: Project-wide configuration and compile options are in `src/Directory.Build.props` and `src/Directory.Build.targets`. These import many supporting files from `src/Targets/` (for packaging, versions, compile options). Before changing build behaviour, inspect `src/Targets/*`.

- **Build outputs and tests**: Binaries and test output are placed under the repo `Build/` folder (see `Build/bin/` and `Build/Coverage`). Coverage uses `reportgenerator` per the root `README.md` (example command in that README).

- **Naming & conventions**:
  - Projects and packages are prefixed with `TheXDS.MCART` / `MCART.*`.
  - Tests generally follow the `*.Tests` project naming convention.
  - UI variants are split into platform-targeted projects (WPF, Avalonia, WinForms, etc.) inside `src/Platform/`.

- **Packaging & publishing**: Packaging/version behavior is driven by files in `src/Targets` and `src/Directory.Build.props`. CI workflows (GitHub Actions) and publish steps are referenced from the root `README.md` and `.github/workflows` (inspect those files when automating releases).

- **Language mix**: The project targets .NET and historically contains utilities for both C# and Visual Basic. Respect existing style in the target project (check the project file and surrounding sources for language-specific patterns).

- **What to change vs what to avoid**:
  - Safe: small fixes in a single library (add method in `src/Lib/MCART.Coloring`, fix tests for `MCART.Coloring.Tests`).
  - Careful: changes to `src/Directory.Build.*`, `src/Targets/*`, and solution-level packaging — these affect all projects.

- **Useful quick commands** (PowerShell / `pwsh.exe`):
  - Build full solution: `dotnet build ./src/MCART.sln`
  - Run tests: `dotnet test ./src/MCART.sln --no-build`
  - Run tests with coverage and generate report (requires ReportGenerator):
    `dotnet test ./src/MCART.sln --collect:"XPlat Code Coverage" --results-directory:./Build/Tests ; reportgenerator -reports:./Build/Tests/*/coverage.cobertura.xml -targetdir:./Build/Coverage/`
  - Build a single project: `dotnet build src/Lib/MCART.Coloring/MCART.Coloring.csproj`

- **Where to look for examples**:
  - `src/Lib/MCART/` - type extensions, core utilities and base classes.
  - `src/Lib/MCART.Mvvm/` and `src/Lib/MCART.UI/` — MVVM patterns and UI helpers.
  - `src/Lib/MCART.Windows/` — Windows-specific utilities, and PInvoke-based interop.
  - `Build/` — compiled outputs used by CI and local testing.

- **When adding code**:
  - Update the appropriate project file under `src/Lib/*`.
  - Add unit tests under the corresponding `*.Tests` project and run `dotnet test` locally.
  - Keep public APIs consistent with existing `MCART.*` naming.
  - Add XML documentation comments for public members/types.

- **PR & review notes for agents**:
  - Keep changes minimal and focused to one concern (single project or fix).
  - If modifying build/packaging, include a clear summary and tests demonstrating the change.

If anything above is unclear or you want more detail about a subsystem (for example: packaging, CI, or the UI projects), tell me which area and I'll expand this file with focused examples.
