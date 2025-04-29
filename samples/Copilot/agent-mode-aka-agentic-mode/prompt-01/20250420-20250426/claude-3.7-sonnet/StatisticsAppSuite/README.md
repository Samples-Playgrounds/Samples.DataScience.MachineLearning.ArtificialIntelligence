# Descriptive Statistics Application Suite

A comprehensive .NET solution demonstrating how to implement and share business logic across multiple application types using a class library. This solution includes implementations of descriptive statistics across various client and server application platforms.

## Solution Structure

This solution consists of the following projects:

### Core Library

- **Statistics.Core** - A class library implementing comprehensive descriptive statistics functionality including mean, median, mode, standard deviation, variance, percentiles, skewness, kurtosis, and more.

### Client Applications

- **Statistics.ConsoleApp** - A command-line application demonstrating statistics functionality with a sample dataset.
- **Statistics.BlazorWasm** - A Blazor WebAssembly client application with an interactive UI for entering and analyzing data.
- **Statistics.MauiApp** - A native MAUI application with UI controls for data entry and statistics display.
- **Statistics.MauiBlazorApp** - A Blazor Hybrid application running on MAUI, combining native capabilities with Blazor components.

### Server Applications

- **Statistics.BlazorApp** - A Blazor Server application with an interactive statistics UI.
- **Statistics.Api** - A REST Web API exposing endpoints to analyze statistical data.

## Features

The implemented descriptive statistics functionality includes:

- Basic statistics: min, max, range, count, sum
- Central tendency measures: mean, median, mode
- Dispersion measures: variance, standard deviation
- Distribution shape: skewness, kurtosis
- Percentiles: 25th, 50th, 75th, and custom percentiles
- Interquartile range (IQR)
- Statistical summaries

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/) with C# extensions
- For MAUI applications: [MAUI workload](https://docs.microsoft.com/en-us/dotnet/maui/get-started/installation)

### Building the Solution

To build the complete solution:

```bash
dotnet build
```

### Running Individual Applications

#### Console Application

```bash
cd Statistics.ConsoleApp
dotnet run
```

#### Web API

```bash
cd Statistics.Api
dotnet run
```

Then navigate to:
- Swagger UI: https://localhost:7042/swagger
- Sample statistics: https://localhost:7042/statistics/sample

#### Blazor Server Application

```bash
cd Statistics.BlazorApp
dotnet run
```

Then navigate to https://localhost:5001

#### Blazor WebAssembly Application

```bash
cd Statistics.BlazorWasm
dotnet run
```

Then navigate to https://localhost:5002

#### MAUI Applications

For MAUI applications, you need to specify a target platform:

```bash
cd Statistics.MauiApp
dotnet build -t:Run -f net6.0-android
```

Or for iOS (requires macOS):

```bash
cd Statistics.MauiApp
dotnet build -t:Run -f net6.0-ios
```

## API Endpoints

The REST API exposes the following endpoints:

- `GET /statistics/sample` - Returns statistics for a sample dataset
- `POST /statistics/analyze` - Analyzes a provided dataset
  - Request body: `{ "data": [1, 2, 3, 4, 5] }`

## Implementation Details

### Statistics.Core

The core library implements a `DescriptiveStatistics` class that takes a dataset as input and calculates various statistical measures. All applications reference this library to ensure consistent calculation logic.

### UI Applications

All UI applications (Blazor Server, Blazor WASM, MAUI, and MAUI Blazor) implement a similar user interface pattern:

1. Input area for entering data
2. Controls for analyzing data, loading samples, and clearing
3. Results display showing calculated statistics
4. Error handling for invalid inputs

## Architecture

This solution demonstrates:

1. **Shared Business Logic**: Core statistics calculations are implemented once in a shared class library.
2. **Cross-platform UI**: The same functionality is available across web, desktop, and mobile platforms.
3. **Multiple Frontend Frameworks**: Demonstrates .NET's versatility with multiple UI technologies.
4. **API-based Architecture**: The Web API exposes statistics functionality for any client to consume.

## License

MIT