# Dive Hub — .NET MAUI (2026)

![Dive Hub](Resources/Images/ocean_hero.jpg)

**A modern, fully offline scuba diving companion built with .NET MAUI.**

## Overview

**Dive Hub** is a modern mobile and desktop application inspired by the original Dive Hub Scuba Diving Center Android application developed in **2014**.

Rebuilt from scratch in **2026** using C#, .NET MAUI, XAML, and SQLite, the application transforms the original Java-based project into a modern, visually engaging scuba diving experience.

The application combines diving destination information, professional diving courses, underwater photography, and a personal digital dive logbook.

Designed with an ocean-inspired interface and an offline-first architecture, Dive Hub does not require a cloud server, internet connection, or user account to access its core functionality.

This project also documents the evolution of my software development experience, from native Android development with Java and Eclipse to modern cross-platform development with Microsoft's .NET ecosystem.

---

## Features

### Home Dashboard

- Modern underwater-inspired interface
- Featured diving destinations
- Diving services overview
- Quick access to major application modules
- Integrated photo previews
- Responsive navigation

### Dive Sites

Explore **14 diving locations** through an interactive catalog featuring:

- Underwater photographs
- Dive site descriptions
- Available depth information
- Experience requirements where documented
- Search and category filters
- Individual dive site detail pages

The collection includes original and additional locations documented in the historical Dive Hub sources.

### Diving Courses

Browse **31 diving courses** across four training organizations:

- **PADI** — Professional Association of Diving Instructors
- **CMAS** — World Underwater Federation
- **DAN** — Divers Alert Network
- **TDI** — Technical Diving International

Course pages provide historical training descriptions, available prerequisites, and related information.

### Diving Services

Explore five categories of scuba diving activities:

- Discover Scuba Diving / Try Dives
- Shore Diving
- Boat Diving
- Night Diving
- Guided Diving

### Underwater Photo Gallery

A locally stored collection of **32 underwater photographs**, featuring:

- Responsive photo grid
- Full-screen image viewing
- Previous and next image navigation
- Authentic Dive Hub underwater photography
- Offline image access

### Digital Dive Logbook

A personal SQLite-powered digital dive logbook designed to organize scuba diving experiences.

**Dive information includes:**

- Dive number and location
- Dive date
- Maximum depth
- Dive duration
- Starting and ending tank pressure
- Exposure protection
- Weight
- Dive leader
- Dive buddy
- Personal notes

**Logbook functionality:**

- Add new dives
- View saved dives
- Edit existing dive records
- Delete dives with confirmation
- Search and sort records
- Validate user input
- Prevent duplicate dive numbers
- Preserve records locally between application sessions

**Dive statistics:**

- Total recorded dives
- Total underwater time
- Maximum recorded depth

All dive records are stored locally using SQLite.

### Dive Hub Information

- Historical diving center information
- Team profiles and photographs
- Original Dive Hub identity
- Archived contact information

---

## Technology Stack

| Technology | Purpose |
|---|---|
| C# | Application development |
| .NET 10 | Application runtime |
| .NET MAUI | Cross-platform application framework |
| XAML | Native user interface |
| MVVM | Presentation architecture |
| SQLite | Local dive logbook database |
| JSON | Bundled diving content |
| MAUI Shell | Application navigation |
| Dependency Injection | Service management |

## Architecture

The application follows a modular structure with separation between the user interface, application logic, data access, and bundled content.

```text
DiveHubMauiApp/
│
├── Components/
├── Data/
├── Models/
├── Services/
├── ViewModels/
├── Views/
│
├── Resources/
│   ├── AppIcon/
│   ├── Fonts/
│   ├── Images/
│   ├── Raw/
│   ├── Splash/
│   └── Styles/
│
├── Platforms/
│   ├── Android/
│   └── Windows/
│
├── docs/
├── tests/
│
├── App.xaml
├── AppShell.xaml
├── MauiProgram.cs
└── DiveHubMauiApp.csproj
```

### Offline-First Design

The application is designed to function without an internet connection.

- Diving destinations and courses are bundled locally.
- Gallery photographs are included in application resources.
- Static information is loaded from local JSON content.
- Dive records are stored in SQLite.
- No backend API or cloud database is required.
- No account registration or authentication is needed.

---

## Getting Started

### Requirements

- .NET 10 SDK
- .NET MAUI workload
- Android SDK and compatible Java Development Kit for Android builds
- Windows development environment for Windows builds

### Clone the Repository

```bash
git clone https://github.com/hakanberat/DiveHubMaui-2026.git
cd DiveHubMaui-2026
```

### Restore Dependencies

```bash
dotnet restore DiveHubMauiApp.csproj
```

### Run on Android

Connect an Android device with USB debugging enabled or start an Android emulator.

```bash
dotnet build DiveHubMauiApp.csproj -f net10.0-android -c Debug -t:Run
```

### Run on Windows

```bash
dotnet run --project DiveHubMauiApp.csproj -f net10.0-windows10.0.19041.0
```

### Run Automated Checks

```bash
dotnet run --project tests/DiveHub.Checks.csproj
```

---

## Development and Modernization

### Original Android Application — 2014

The original application was developed for Dive Hub Scuba Diving Center using:

- Java
- Android SDK
- Eclipse IDE
- XML layouts
- SQLite
- Android Activities and Intents

It provided diving center information, course descriptions, diving locations, underwater photographs, contact functionality, and an early digital logbook implementation.

**Original repository:**

[DiveHub-Android-Legacy](https://github.com/hakanberat/DiveHub-Android-Legacy)

### Modern .NET MAUI Application — 2026

The new application is a complete architectural and visual redesign.

Key improvements include:

- Cross-platform development with .NET MAUI
- Modern C# implementation
- MVVM architecture
- Improved data validation
- Enhanced SQLite persistence
- Responsive and reusable UI components
- Expanded diving content
- Redesigned photo gallery
- Improved navigation
- Fully offline operation

Rather than directly translating the original Java code, the project rebuilds the functionality using modern .NET development practices.

---

## Design Philosophy

Dive Hub follows a minimalist, ocean-inspired design language.

The interface emphasizes underwater photography, clean typography, intuitive navigation, and a consistent visual experience.

**Design principles:**

- Ocean-inspired color palette
- Dark navy and turquoise accents
- Photography-focused layouts
- Reusable components
- Clear information hierarchy
- Responsive interfaces
- Minimal external dependencies

---

## Project Status

**2026 — Modernization release**

Implemented application modules include the home dashboard, dive sites, diving courses, services, gallery, historical information, and offline logbook.

Android and Windows builds and automated checks have been validated during development. Android device-specific behavior should still be independently verified on physical hardware.

This project is maintained as a software development portfolio application and historical modernization project.

It is not a live reservation platform. Historical diving center information and course descriptions should not be interpreted as current availability, pricing, or certification standards.

Additional technical documentation is available in:

- [Content Sources](docs/sources.md)
- [Validation and Testing](docs/validation.md)
- [Asset Manifest](docs/asset-manifest.json)

---

## Related Projects

**Original Android Application (2014)**

https://github.com/hakanberat/DiveHub-Android-Legacy

**Dive Hub Logbook — .NET MAUI**

https://github.com/hakanberat/DiveHubLogbook

**Dive Center Management System — ASP.NET Core**

https://github.com/hakanberat/Dive-Center-Management-System

---

## Developer

**Hakan Berat Demircan, PhD**

Software Developer | PADI IDC Staff Instructor | Underwater Videographer

Background in Information Technology, mobile application development, scuba diving operations, and tourism management.

**GitHub:** https://github.com/hakanberat

**Portfolio:** https://hakanberat.github.io

---

*Originally conceived in 2014. Reimagined with .NET MAUI in 2026.*

**Dive Hub — Explore What Lies Beneath.**
