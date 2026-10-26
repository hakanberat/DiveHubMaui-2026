# Validation — 26 October 2026

## Build environment

- Windows x64 development host.
- Stable .NET SDK 10.0.300, selected by `global.json`.
- Installed MAUI 10.0.20 workload; Android API 36 SDK and local JDK.
- Project directly in the requested workspace; no nested project root.
- NuGet restore completed. Sandbox restrictions on machine NuGet settings and Release toolchain cache writes were resolved by running the necessary build/restore commands with authorized host access.

## Automated verification

`dotnet run --project tests/DiveHub.Checks.csproj --no-restore`: **42 checks passed**.

Coverage includes actual SQLite insert/read/update/delete, all persisted field types, apostrophes/SQL-like input, restart persistence, duplicate inserts and edits, concurrent uniqueness, numeric/date sorting, search, dashboard totals, invalid values and NaN, schema-version preservation, database triggers and integrity, 14 sites/31 courses/five services/32 gallery photos/five team members, complete descriptions/sources, all referenced local images, XAML/resource references and the absence of application network clients.

The tests create isolated databases under `.reference/`; they do not alter a user's logbook.

## Builds

- Windows Debug: successful, zero warnings and zero errors.
- Android Debug: successful, zero warnings and zero errors.
- Android Release: successful, zero warnings and zero errors; linked and AOT-compiled.
- No publication, deployment or cloud resources.

A clean Release APK was inspected: all 48 original source images and the 50-entry catalog are bundled; no legacy/reference files or template assets remain. The Android Release merged manifest was inspected: no `android.permission.INTERNET` or `android.permission.ACCESS_NETWORK_STATE`. AndroidX adds its normal app-local dynamic-receiver permission. Debug tooling injects Internet permission into Debug builds.

## Desktop inspection

The compiled Windows app was launched and inspected with native computer-use tooling. Verified the dashboard and original photos, adaptive collection cards, local search returning Ray Cave, site-detail navigation, logbook dashboard/empty state, the complete editor including notes/save/cancel controls, the gallery grid, and full-screen next/previous image navigation (01/32 → 02/32 → 01/32). An empty editor save was rejected with visible validation feedback and automatic scroll to the error; no test dive was added to the app logbook. Keyboard-accessible Explore/View buttons are included. The native date control was verified as English (26 Oct 2026). Initial window bounds are capped to the display size.

Desktop inspection is a smoke test, not an exhaustive device matrix. SQLite correctness and query behavior were exercised separately against the actual shared implementation.

## Remaining device checks

ADB reports no connected Android devices, and no emulator installation was found. The app was **not physically tested on Android**. Airplane-mode navigation, Android back/keyboard behavior, full-screen gallery gestures, and save/edit/delete across process restarts should be checked on the intended phone.

Windows Release packaging, store signing, installation on another PC and device-specific accessibility/font-scale matrices were not performed. Windows Debug and Android Release are available locally for the user's testing.

The static collection has the documented source gaps in [sources.md](sources.md). No known build errors remain.

