
# CSG Pro Hackathon API

This project was created to support the CSG Pro 2014 Hackathon. Hackathon participants created a client app, on platform of their choice, that used this API for the back end. The API provides a simple back end service for creating time tracking applications.

## API Design

The API exposes the following types:

* User
* Project
* Project Role
* Project Task
* Time Entry

Two modes of time tracking are available: "Stopwatch" and "Explicit Sums".

With "Stopwatch" time tracking, the end user is intended to create time tracking entries throughout the day, as they occur. In this mode, the act of creating a new time tracking entry updates the previous time tracking entry with an end time and matches the start time of the new time tracking entry.

With "Explicit Sums" time tracking, the end user is intended to enter their time tracking entries at the end of the day. In this mode, the user provides a "total time" value for each entry, eliminating the need for each entry to have a start and end time.

## Technical Overview

The API is created using the ASP.NET Web API 2 framework. Entity Framework was used for all data access.

A SQL Server 2014 LocalDb "v12.0" instance is required in order to run the API locally.

The API is secured using Basic Authentication.
