# Compact UI and Taiwan Holidays Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Resize the fixed WinForms UI to 720 x 660 and suppress automatic UOF attendance actions on official Taiwan government holidays using a validated, once-per-year local CSV cache.

**Architecture:** A provider discovers the requested ROC-year CSV through the official data.gov.tw dataset metadata endpoint, validates it with a focused parser, and stores it beside the executable. `DailyCheck` consumes an injected provider and delegates the weekday-plus-holiday decision to a pure policy; manual actions remain untouched.

**Tech Stack:** C# 7.x, .NET Framework 4.7.2, WinForms, `HttpClient`, `System.Web.Script.Serialization.JavaScriptSerializer`, Visual Studio 2022 MSBuild, dependency-free console test harness.

## Global Constraints

- The main client area is exactly 720 x 660 and remains `FixedSingle`.
- Cache files are `HolidayData/holidays-YYYY.csv` beside the executable.
- Official dataset ID is `14718`; holiday flag `2` means a day off.
- A selected weekday is suppressed by a holiday; holiday data never enables an unselected weekday or weekend make-up workday.
- Missing holiday data falls back to the existing weekday-only behavior and produces a visible status message.
- Manual check-in/out, randomized time windows, five-minute retry behavior, credentials, tray behavior, and Task Scheduler behavior remain unchanged.
- Network, metadata, CSV, and filesystem failures are logged without terminating automatic scheduling.
- Use full-framework Visual Studio 2022 MSBuild for Debug and Release verification.

---

## File Map

- Create `HolidayCalendarResult.cs`: immutable availability and holiday-date result.
- Create `ITaiwanHolidayProvider.cs`: asynchronous year-based provider contract.
- Create `TaiwanHolidayCsvParser.cs`: validates official CSV and extracts flag-2 dates.
- Create `OfficialTaiwanHolidayProvider.cs`: metadata discovery, download, annual file cache, fallback.
- Create `AutomaticWorkdayPolicy.cs`: pure weekday/holiday eligibility rule.
- Create `AutoCheckIn.Tests/AutoCheckIn.Tests.csproj`: dependency-free test executable.
- Create `AutoCheckIn.Tests/Program.cs`: focused assertions and fakes.
- Modify `Properties/AssemblyInfo.cs`: expose internals to the test executable.
- Modify `DailyCheck.cs`: load annual calendar and apply the policy before timing checks.
- Modify `Program.cs`: construct the official provider and inject it.
- Modify `MainForm.cs`: update designer-only fallback construction for the new dependency.
- Modify `newForm1.Designer.cs`: compact positions and sizes only.
- Modify `AutoCheckIn.csproj` and `AutoCheckIn.sln`: compile new production files, add `System.Web.Extensions`, and include the test project.

### Task 1: Holiday CSV domain model and parser

**Files:**
- Create: `HolidayCalendarResult.cs`
- Create: `ITaiwanHolidayProvider.cs`
- Create: `TaiwanHolidayCsvParser.cs`
- Create: `AutoCheckIn.Tests/AutoCheckIn.Tests.csproj`
- Create: `AutoCheckIn.Tests/Program.cs`
- Modify: `Properties/AssemblyInfo.cs`
- Modify: `AutoCheckIn.csproj`
- Modify: `AutoCheckIn.sln`

**Interfaces:**
- Produces: `Task<HolidayCalendarResult> ITaiwanHolidayProvider.GetCalendarAsync(int year, CancellationToken cancellationToken)`; the interface also inherits `IDisposable`.
- Produces: `HolidayCalendarResult TaiwanHolidayCsvParser.Parse(string csv, int expectedYear)`.
- Produces: `HolidayCalendarResult.Available`, `HolidayCalendarResult.Holidays`, and `HolidayCalendarResult.Message`.

- [x] **Step 1: Add the failing parser tests and minimal test runner**

Create a console test runner whose `Main` executes named `Action` tests, prints `PASS <name>`, prints the exception for failures, and returns `1` if any test fails. Add these exact cases:

```csharp
private static void ParserReadsOnlyFlagTwoDates()
{
    string csv = BuildCalendarCsv(2026, new DateTime(2026, 1, 1));
    var result = TaiwanHolidayCsvParser.Parse(csv, 2026);
    AssertTrue(result.Available, "calendar should be available");
    AssertTrue(result.Holidays.Contains(new DateTime(2026, 1, 1)), "holiday missing");
    AssertFalse(result.Holidays.Contains(new DateTime(2026, 1, 2)), "workday included");
}

private static void ParserRejectsWrongYearAndMalformedFiles()
{
    AssertThrows<FormatException>(() =>
        TaiwanHolidayCsvParser.Parse(BuildCalendarCsv(2025), 2026));
    AssertThrows<FormatException>(() =>
        TaiwanHolidayCsvParser.Parse("wrong,headers\r\nvalue,value\r\n", 2026));
}
```

`BuildCalendarCsv(int year, params DateTime[] holidays)` must emit the official header followed by every date in that year and flag only the supplied dates with `2`; reuse it as `Valid2026Csv` in later provider tests. Mark the runner's `Main` with `[STAThread]` so the later WinForms layout test runs correctly.

The test project targets v4.7.2, outputs an executable, references `System`, `System.Core`, and the production project, and uses assembly name `AutoCheckIn.Tests`. Add `[assembly: InternalsVisibleTo("AutoCheckIn.Tests")]`.

- [x] **Step 2: Build to verify the parser tests fail**

Run:

```powershell
& 'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe' AutoCheckIn.sln /t:Rebuild /p:Configuration=Debug
```

Expected: compilation fails because `TaiwanHolidayCsvParser` and `HolidayCalendarResult` do not exist.

- [x] **Step 3: Implement the result, interface, and strict parser**

Implement the contracts exactly:

```csharp
internal sealed class HolidayCalendarResult
{
    public HolidayCalendarResult(bool available, IEnumerable<DateTime> holidays, string message)
    {
        Available = available;
        Holidays = new HashSet<DateTime>((holidays ?? Enumerable.Empty<DateTime>()).Select(d => d.Date));
        Message = message ?? string.Empty;
    }

    public bool Available { get; }
    public HashSet<DateTime> Holidays { get; }
    public string Message { get; }

    public static HolidayCalendarResult Unavailable(string message) =>
        new HolidayCalendarResult(false, Enumerable.Empty<DateTime>(), message);
}

internal interface ITaiwanHolidayProvider : IDisposable
{
    Task<HolidayCalendarResult> GetCalendarAsync(
        int year, CancellationToken cancellationToken = default(CancellationToken));
}
```

`Parse` must remove a UTF-8 BOM, require the four official headers, split records with `StringReader`, accept `yyyyMMdd` and `yyyy/M/d`, require every parsed record to match `expectedYear`, require 365 or 366 unique dates so truncated downloads cannot become valid cache files, and add only rows whose holiday field equals `2`. CSV fields may be quoted; implement a small quote-aware `ParseLine` that supports commas and doubled quotes.

- [x] **Step 4: Build and run the parser tests**

Run the Debug rebuild command, then:

```powershell
& '.\AutoCheckIn.Tests\bin\Debug\AutoCheckIn.Tests.exe'
```

Expected: both parser cases print `PASS` and the process exits `0`.

- [x] **Step 5: Commit the parser boundary**

```powershell
git add HolidayCalendarResult.cs ITaiwanHolidayProvider.cs TaiwanHolidayCsvParser.cs AutoCheckIn.Tests Properties/AssemblyInfo.cs AutoCheckIn.csproj AutoCheckIn.sln
git commit -m "Add Taiwan holiday calendar parser"
```

### Task 2: Official discovery and annual file cache [DONE, commit 4dbe891 — see note below]

**Files:**
- Create: `OfficialTaiwanHolidayProvider.cs`
- Modify: `AutoCheckIn.Tests/Program.cs`
- Modify: `AutoCheckIn.csproj`

**Interfaces:**
- Consumes: `TaiwanHolidayCsvParser.Parse(string csv, int expectedYear)`.
- Produces: `OfficialTaiwanHolidayProvider(string cacheDirectory, Func<Uri, CancellationToken, Task<string>> downloadTextAsync)` for deterministic tests.
- Produces: `OfficialTaiwanHolidayProvider.CreateDefault(string cacheDirectory)` for runtime use.

> **Note (added after implementation):** the exact JSON shape of the data.gov.tw v2 dataset metadata endpoint (`result.distribution[]` with `resourceDescription`/`resourceFormat`/`resourceDownloadUrl`/`resourceCharacterEncoding` fields) was assumed from typical v2 REST dataset API conventions, not verified against a live response -- this session had no test credentials/network path confirmed against the real endpoint. All tests here use an injected fake matching this assumed shape, so they prove the *parsing/selection logic* is correct against that shape, not that the shape itself is correct. Verifying `OfficialTaiwanHolidayProvider.CreateDefault` against the real endpoint once is a required manual step before this ships.


- [x] **Step 1: Add failing cache and discovery tests**

Use a fresh directory below `Path.GetTempPath()` per test and delete that exact directory in `finally`. Add tests that prove:

```csharp
private static async Task ProviderUsesValidCacheWithoutNetworkAsync()
{
    File.WriteAllText(Path.Combine(testDirectory, "holidays-2026.csv"), Valid2026Csv, Encoding.UTF8);
    int calls = 0;
    var provider = new OfficialTaiwanHolidayProvider(testDirectory, (uri, token) =>
    {
        calls++;
        throw new InvalidOperationException("network should not be called");
    });
    var result = await provider.GetCalendarAsync(2026, CancellationToken.None);
    AssertTrue(result.Available, "cache should load");
    AssertEqual(0, calls, "network call count");
}
```

Also add `ProviderDiscoversDownloadsAndCachesAsync`: first fake response is metadata JSON containing distributions for a Google calendar and `115年中華民國政府行政機關辦公日曆表`; second response is `Valid2026Csv`. Assert two calls, the requested CSV URI, and creation of `holidays-2026.csv`. Add `ProviderFallsBackWhenUnavailableAsync`: throw `HttpRequestException`, assert `Available == false` and a non-empty fallback message.

- [x] **Step 2: Run tests to verify the provider cases fail**

Run the Debug rebuild and test executable.

Expected: compilation fails because `OfficialTaiwanHolidayProvider` does not exist.

- [x] **Step 3: Implement official metadata discovery and cache**

Use constants and selection rules:

```csharp
private static readonly Uri MetadataUri =
    new Uri("https://data.gov.tw/api/v2/rest/dataset/14718");

private static string ExpectedDescription(int year) =>
    (year - 1911).ToString(CultureInfo.InvariantCulture) + "年中華民國政府行政機關辦公日曆表";
```

Deserialize metadata with `JavaScriptSerializer` into dictionaries. Select a `distribution` whose `resourceDescription` equals the expected description, `resourceFormat` equals `CSV`, and description does not contain `Google`; if exact text is unavailable, accept a description that starts with the ROC year and does not contain `Google`, preferring `resourceCharacterEncoding == "UTF-8"`. Require an absolute HTTPS `resourceDownloadUrl`.

The flow is: parse valid cache and return; otherwise download metadata; resolve resource; download CSV; parse fully; create `cacheDirectory`; write UTF-8 to `holidays-YYYY.csv`; return parsed result. Catch non-cancellation exceptions, log with `ErrorLogger.Log("Taiwan holiday calendar", ex)`, and return `HolidayCalendarResult.Unavailable("Holiday data unavailable; using weekday schedule only.")`. Re-throw `OperationCanceledException` when the supplied token is cancelled. Add `System.Web.Extensions` to production references.

`CreateDefault` owns one `HttpClient` and supplies a delegate that calls `GetStringAsync` through a cancellation-aware `HttpRequestMessage`/`SendAsync` helper; implement `IDisposable` on the provider so the owned client is deterministically disposed while injected delegates are not.

- [x] **Step 4: Rebuild and run all provider tests**

Run the Debug rebuild and test executable.

Expected: parser and all three provider cases print `PASS`; process exits `0`.

- [x] **Step 5: Commit official holiday caching**

```powershell
git add OfficialTaiwanHolidayProvider.cs AutoCheckIn.Tests/Program.cs AutoCheckIn.csproj
git commit -m "Cache official Taiwan holiday calendars"
```

### Task 3: Apply holiday eligibility to automatic scheduling

**Files:**
- Create: `AutomaticWorkdayPolicy.cs`
- Modify: `DailyCheck.cs`
- Modify: `Program.cs`
- Modify: `MainForm.cs`
- Modify: `AutoCheckIn.Tests/Program.cs`
- Modify: `AutoCheckIn.csproj`

**Interfaces:**
- Consumes: `ITaiwanHolidayProvider.GetCalendarAsync(int, CancellationToken)`.
- Produces: `bool AutomaticWorkdayPolicy.IsEligible(DateTime date, bool weekdaySelected, HolidayCalendarResult calendar)`.
- Changes constructor to `DailyCheck(IUofClient uofClient, ITaiwanHolidayProvider holidayProvider)`.

- [ ] **Step 1: Add failing policy tests**

Add these exact assertions:

```csharp
var holiday = new DateTime(2026, 10, 9);
var available = new HolidayCalendarResult(true, new[] { holiday }, "loaded");
var unavailable = HolidayCalendarResult.Unavailable("fallback");
AssertFalse(AutomaticWorkdayPolicy.IsEligible(holiday, true, available), "selected holiday");
AssertTrue(AutomaticWorkdayPolicy.IsEligible(holiday.AddDays(1), true, available), "ordinary selected day");
AssertFalse(AutomaticWorkdayPolicy.IsEligible(holiday, false, unavailable), "unselected day");
AssertTrue(AutomaticWorkdayPolicy.IsEligible(holiday, true, unavailable), "weekday-only fallback");
```

Add a fake provider that records requested years. Exercise an internal `DailyCheck.EnsureHolidayCalendarAsync(DateTime, CancellationToken)` twice in 2026 and once in 2027; assert requests are exactly `2026, 2027`.

- [ ] **Step 2: Run tests to verify policy and scheduler integration fail**

Run the Debug rebuild and test executable.

Expected: compilation fails because `AutomaticWorkdayPolicy`, the new constructor, and `EnsureHolidayCalendarAsync` do not exist.

- [ ] **Step 3: Implement the pure rule and yearly scheduler loading**

Implement the policy exactly:

```csharp
internal static bool IsEligible(
    DateTime date, bool weekdaySelected, HolidayCalendarResult calendar)
{
    if (!weekdaySelected) return false;
    return calendar == null || !calendar.Available || !calendar.Holidays.Contains(date.Date);
}
```

In `DailyCheck`, store the injected provider, current calendar, and loaded year. `EnsureHolidayCalendarAsync` loads only when the year changes, then updates those fields under `m_stateLock`. Call it before entering the scheduling lock in `CheckScheduleAsync`.

Within the lock, preserve the existing weekday check first. Then call `AutomaticWorkdayPolicy.IsEligible`. For a known holiday set status to `"Today is a Taiwan government holiday."` and return. When data is unavailable, allow existing timing logic and retain `calendar.Message` until an attendance result replaces it. Reset the loaded year only on year changes, not on every daily reset. Dispose the provider from `DailyCheck.Dispose()` after `Stop()`.

Update runtime and designer composition to use:

```csharp
var holidayDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HolidayData");
var holidayProvider = OfficialTaiwanHolidayProvider.CreateDefault(holidayDirectory);
var scheduler = new DailyCheck(
    new CredentialSavingUofClient(new CrackClient(), credentialStore), holidayProvider);
```

- [ ] **Step 4: Rebuild and run all automated tests**

Run the Debug rebuild and test executable.

Expected: all parser, provider, policy, and yearly-load cases print `PASS`; process exits `0`.

- [ ] **Step 5: Commit scheduler holiday awareness**

```powershell
git add AutomaticWorkdayPolicy.cs DailyCheck.cs Program.cs MainForm.cs AutoCheckIn.Tests/Program.cs AutoCheckIn.csproj
git commit -m "Skip automatic checks on Taiwan holidays"
```

### Task 4: Compact the WinForms layout and complete verification

**Files:**
- Modify: `newForm1.Designer.cs`

**Interfaces:**
- Consumes: existing protected controls used by `MainForm`.
- Produces: the same fixed layout at a 720 x 660 client size; no behavioral API changes.

- [ ] **Step 1: Record the failing layout expectation**

Add a test that constructs `newForm1` on an STA thread and asserts:

```csharp
AssertEqual(new Size(720, 660), form.ClientSize, "client size");
AssertEqual(FormBorderStyle.FixedSingle, form.FormBorderStyle, "border style");
```

- [ ] **Step 2: Run the test and verify the current 820 x 742 layout fails**

Run the Debug rebuild and test executable.

Expected: the layout case fails with actual client size `820, 742`.

- [ ] **Step 3: Apply the compact layout dimensions**

Change only designer layout properties. Use these bounding rectangles:

```text
Form           (0,   0, 720, 660)
headerPanel    (0,   0, 720,  92)
accountPanel   (30,112, 321, 182)
manualPanel    (369,112,321, 182)
schedulePanel  (30,310, 660, 164)
startupPanel   (30,490, 660,  80)
statusPanel    (30,586, 660,  46)
```

Within panels, reduce horizontal dimensions while retaining at least 18 px internal margins: account text boxes become 275 px wide; quick-action buttons become 275 px wide; schedule auto button becomes 198 px wide and begins at x=438; startup status becomes 390 px wide; ADD/REMOVE buttons become 92 px wide at x=438 and x=544. Reduce large vertical gaps and button heights proportionally, keep labels readable, and preserve all names, tags, colors, text, docking, event wiring, and control order.

- [ ] **Step 4: Rebuild Debug and run automated tests**

Run the Debug rebuild and test executable.

Expected: all tests, including the exact 720 x 660 layout case, pass.

- [ ] **Step 5: Rebuild Release**

```powershell
& 'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe' AutoCheckIn.sln /t:Rebuild /p:Configuration=Release
```

Expected: `Build succeeded`, with zero errors.

- [ ] **Step 6: Perform focused manual checks**

Run `bin\Debug\AutoCheckIn.exe` and verify the complete UI fits without clipping at 100% Windows scaling. Start automatic mode with a fake or non-production-safe test boundary only; confirm status distinguishes a holiday, an ordinary selected weekday, and unavailable holiday data. Run with `-a`, minimize/restore through the tray, reload encrypted credentials, and exercise Task Scheduler ADD/UPDATE/REMOVE without changing their existing behavior. Never commit `credentials.dat` or generated `HolidayData` files.

- [ ] **Step 7: Commit the compact GUI**

```powershell
git add newForm1.Designer.cs AutoCheckIn.Tests/Program.cs
git commit -m "Compact the main window layout"
```

- [ ] **Step 8: Inspect final repository state**

```powershell
git status --short
git log -5 --oneline
```

Expected: no unintended files; only intentionally untracked local runtime data may remain and must not be committed.
