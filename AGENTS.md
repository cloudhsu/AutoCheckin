# Repository Guidelines

## Project Structure & Module Organization

This is a C# WinForms application targeting .NET Framework 4.7.2. `Program.cs` is the composition root and starts `MainForm`. UI layouts are defined in `newForm*.cs` and their `.Designer.cs` files; `MainForm` currently reuses the `newForm1` layout. UOF communication is isolated behind `IUofClient` in `CrackClient.cs`, while `DailyCheck.cs` owns scheduling and retry behavior. Credential persistence and Windows startup-task management live in `DpapiCredentialStore.cs` and `WindowsStartupTaskService.cs`. Images and generated settings are under `Resources/` and `Properties/`. The original v0.7 layout is retained under `LegacyLayouts/` and must not be compiled.

## Build, Test, and Development Commands

Use Visual Studio 2022 or its full-framework MSBuild:

```powershell
& 'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe' AutoCheckIn.sln /t:Rebuild /p:Configuration=Debug
& 'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe' AutoCheckIn.sln /t:Rebuild /p:Configuration=Release
```

Run locally with `bin\Debug\AutoCheckIn.exe`; pass `-a` to test automatic schedule startup. `dotnet msbuild` may fail on WinForms resource generation, so prefer Visual Studio MSBuild.

## Coding Style & Naming Conventions

Use four-space indentation and Allman braces. Use `PascalCase` for types, properties, and public methods; use descriptive private field names consistent with nearby code. Keep UI, scheduling, persistence, and UOF transport separated through interfaces. Prefer `async` network operations, cancellation tokens, result objects, and deterministic disposal with `using`. Do not hand-edit generated resource files. Make Designer changes carefully and never add business logic to `.Designer.cs`.

## Testing Guidelines

No automated test project currently exists. Every change must compile in Debug and Release. Manually verify manual check-in/out, `-a` startup, encrypted credential reload, tray behavior, and Task Scheduler ADD/UPDATE/REMOVE flows. Never use real credentials in committed test data.

## Commit & Pull Request Guidelines

History uses short imperative subjects, for example `Store encrypted credentials beside executable`. Keep each commit focused. Pull requests should explain behavior changes, verification performed, security implications, and include screenshots for layout changes. Link relevant issues and call out UOF page-field changes explicitly.

## Security & Configuration

Credentials are DPAPI-encrypted into `credentials.dat` beside the executable. Never log passwords or commit this file. Task Scheduler actions must use the absolute executable path and the `-a` argument. Preserve graceful handling of HTTP, HTML parsing, filesystem, and scheduler failures.
