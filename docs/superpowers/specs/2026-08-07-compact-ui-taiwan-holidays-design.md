# Compact UI and Taiwan Holiday Awareness

## Goal

Reduce the main WinForms window to a comfortable medium size and prevent automatic check-in and check-out on Taiwan government holidays, even when the corresponding weekday is selected.

## Scope

- Reduce the fixed main window from 820 x 742 to approximately 720 x 660.
- Preserve the existing visual style, controls, and interaction flow.
- Download one official Taiwan government office calendar CSV per calendar year.
- Cache each downloaded calendar as `HolidayData/holidays-YYYY.csv` beside the executable.
- Exclude official holidays from automatic check-in and check-out.
- Leave manual check-in and check-out unchanged.
- Do not treat weekend make-up workdays as selected workdays unless the user has selected that weekday. The current UI exposes Monday through Friday only, so weekend make-up workdays remain excluded.

## UI Design

`newForm1` remains the layout inherited by `MainForm`. Its client area will be approximately 720 x 660. Header height, outer margins, panel dimensions, control spacing, text boxes, and buttons will be reduced proportionally while retaining readable Segoe UI fonts and the existing fixed single border.

No controls or settings will be added. Holiday loading and fallback messages use the existing status label so the smaller layout does not gain additional visual complexity.

## Holiday Data Source

The application will use the official "Republic of China Government Administrative Agency Office Calendar" dataset published by Taiwan's Directorate-General of Personnel Administration on the Government Data Open Platform (dataset 14718).

The source data includes Gregorian date, weekday, holiday flag, and notes. A holiday flag value of `2` means the date is a day off. Only those flagged dates will suppress automatic attendance actions.

The data provider will discover the current year's official UTF-8 CSV resource instead of relying on a hard-coded annual download URL. Source discovery, downloading, parsing, and cache access will be isolated behind a holiday-calendar interface so changes to the government site do not affect scheduling logic.

## Cache and Refresh Rules

For a requested year, the holiday provider will:

1. Look for `HolidayData/holidays-YYYY.csv` beside the executable.
2. Validate and parse it if present, without performing a network request.
3. If it is absent or invalid, discover and download that year's official CSV.
4. Validate the downloaded content before replacing or creating the cache file.
5. Report holiday data as unavailable if the download fails and no valid cached data exists.

This is a once-per-year cache policy. The application will not repeatedly download a valid file during the same year.

## Scheduling Data Flow

At scheduler startup and when the date crosses into a new year, the scheduler requests the holiday set for the relevant year. A schedule check follows this order:

1. Confirm the current weekday is selected.
2. Confirm the date is not an official holiday.
3. Apply the existing randomized check-in/check-out windows and retry behavior.

If the date is an official holiday, both automatic actions are skipped and the status says that today is a Taiwan government holiday. Manual actions bypass the holiday check.

## Failure Handling

- Network, discovery, file, and CSV parsing failures are caught and logged without terminating the scheduler or UI.
- If holiday data cannot be obtained and no valid annual cache exists, automatic scheduling continues using weekday selections only.
- The existing status label tells the user holiday data is unavailable and that weekday-only scheduling is active.
- Credentials are never included in holiday requests, cache files, or logs.
- Cancellation tokens are propagated through holiday network operations.

## Component Boundaries

- `ITaiwanHolidayProvider`: returns holiday availability and the holiday dates for a requested year.
- Official holiday provider: owns official resource discovery, HTTP download, CSV parsing, annual cache validation, and fallback.
- `DailyCheck`: consumes the provider and decides whether the current date is eligible before applying existing attendance timing.
- `Program`: constructs and injects the provider.
- `newForm1.Designer.cs`: contains only layout dimension and position changes.

## Verification

Automated tests will cover the isolated holiday and scheduling rules where practical, including:

- a selected ordinary weekday is eligible;
- a selected weekday marked with holiday flag `2` is skipped;
- an unselected weekday remains skipped regardless of calendar data;
- a weekend make-up workday does not override weekday selection;
- a valid annual cache avoids a network download;
- missing, malformed, or unavailable data falls back to weekday-only behavior;
- a year boundary requests the new year's calendar.

The solution must rebuild successfully in Debug and Release using Visual Studio 2022 full-framework MSBuild. Manual verification will cover the resized layout at normal Windows scaling, manual actions, automatic startup with `-a`, tray behavior, credential reload, and Task Scheduler controls.
