# MauiPascal

**MauiPascal** is a cross-platform public transit application developed as a school project. It is built with **.NET MAUI** and serves as a frontend demonstration for the [blaise](https://github.com/vincbro/blaise) transit engine.

This project showcases how to build a modern, native UI that consumes a API (*blaise*) to provide offline-capable* transit solutions.

## Features

- **Smart Station Search**:
    - Finds stations instantly using *blaise*'s fuzzy search algorithms.
    - Displays intelligent suggestions with station identifiers.


- **Journey Planning**:
    - **Flexible Scheduling**: Plan trips by "Departure at" or "Arrival by" times.
    - **Quick Controls**: Includes a "Now" button for immediate trips and a "Swap" button to reverse your route.
    - **Date & Time**: Native picker integration for precise scheduling.


- **Detailed Routing**:
    - **Timeline Visualization**: A clear, vertical timeline showing every stop, transfer, and duration.
    - **Trip Summary**: View total travel time and transfer counts at a glance.
    - **Mode Badges**: Visual indicators for different transport modes (e.g., Bus, Train).


- **Cross-Platform**:
    - Runs on **Android**, **iOS**, **macOS** (Catalyst), and **Windows**.
    - Fully adaptive UI supporting both Light and Dark system themes.



## Tech Stack

- **Frontend**: [.NET MAUI](https://dotnet.microsoft.com/en-us/apps/maui) (Targeting .NET 10).
- **MVVM**: Implemented using `CommunityToolkit.Mvvm`.
- **API**: [Blaise](https://github.com/vincbro/blaise) (Custom Rust-based RAPTOR engine).
- **Networking**: `HttpClient` with `Microsoft.AspNetCore.WebUtilities` for query construction.

## Blaise Integration

MauiPascal interacts with the **blaise** API to offload complex routing calculations. The integration is handled in `BlaiseService.cs`:

- **Station Search**: Calls `GET /search/area` to retrieve locations.
- **Pathfinding**: Calls `GET /routing` to calculate the best itinerary based on the RAPTOR algorithm.

## Getting Started

### Prerequisites

1. **.NET SDK**: The project targets `net10.0`.
2. **Blaise Server**: A running instance of the Blaise API is required for data.
*Check the [blaise Repository](https://github.com/vincbro/blaise) for setup instructions.*



### Configuration

Update the API endpoint in `MauiProgram.cs` to point to your local or hosted Blaise instance:

```csharp
builder.Services.AddHttpClient<BlaiseService>(client =>
{
    // Android Emulator: http://10.0.2.2:8080
    // Windows/macOS: http://localhost:8080
    client.BaseAddress = new Uri("http://localhost:8080"); 
});

```

### Running the App

```bash
# Run on Windows
dotnet build -t:Run -f net10.0-windows10.0.19041.0

# Run on Android
dotnet build -t:Run -f net10.0-android
```
