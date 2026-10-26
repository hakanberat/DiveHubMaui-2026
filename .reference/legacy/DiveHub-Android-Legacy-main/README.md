# DiveHub Android Legacy

**Originally developed: 2014**

Legacy Android application developed for **Dive Hub Scuba Diving Center**.

The application combines dive center information, diving services, training course content, a photo gallery, contact functionality, and an offline digital dive logbook.

> This repository is preserved as a legacy project representing my early Android development work.

## Features

### Dive Center Information

- Dive Hub information
- Team information
- Diving services
- Contact information
- Photo gallery

### Dive Sites

The application contains information about multiple local dive sites, including:

- Dive Hub Shore
- Amphora Beach
- Boğaz Shipwreck
- Alp
- Bedri
- Pitos
- Noah's Ark
- Ray Cave
- Abdulselam
- Amphora Wall

### Diving Courses

Course information is available for multiple training organizations:

- PADI
- CMAS
- DAN

### Diving Services

The application includes information about:

- Try dives
- Shore dives
- Boat dives
- Night dives

## Digital Dive Logbook

The application includes an offline digital dive logbook backed by SQLite.

Users can store dive information such as:

- Dive number
- Dive site
- Maximum depth
- Dive time
- Date
- Starting tank pressure
- Ending tank pressure
- Exposure protection
- Weight
- Dive leader
- Buddy
- Notes

Dive records can also be listed and deleted locally on the device.

## Technology Stack

- Java
- Android SDK
- Eclipse
- XML layouts
- SQLite
- Android Activities
- Android Intents
- Local device storage

The original application targeted **Android API 18**.

## Architecture

Android Application
        |
        |-- Static Dive Center Content
        |-- Dive Sites
        |-- Courses
        |-- Photo Gallery
        |-- Contact Functionality
        `-- SQLite Digital Logbook

## Project Status

This project is a **legacy application originally developed in 2014** and is no longer actively maintained.

It is preserved as part of my software development portfolio to demonstrate my earlier experience with:

- Native Android development
- Java
- XML-based user interfaces
- SQLite
- Local data persistence
- Mobile application navigation
- Basic application architecture

## Security Notice

The original application used an outdated approach for sending email directly from the Android client.

Credentials should never be stored directly inside a mobile application.

A modern implementation should use a secure backend architecture such as:

.NET MAUI
    |
    v
ASP.NET Core Web API
    |
    |-- Database
    `-- Email Service

with HTTPS, secure secret storage, authentication, and modern API practices.

## Legacy Project

This repository is intentionally kept in its original architecture to document the evolution of my software development experience.

A future successor could modernize the application using **.NET MAUI, ASP.NET Core Web API, and SQL Server**.
