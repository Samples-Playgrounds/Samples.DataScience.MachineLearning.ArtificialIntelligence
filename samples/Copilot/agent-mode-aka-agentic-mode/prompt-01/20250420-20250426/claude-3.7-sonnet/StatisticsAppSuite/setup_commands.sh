#!/bin/bash

# Create main solution directory
mkdir -p StatisticsAppSuite
cd StatisticsAppSuite

# Create new .NET solution
dotnet new sln -n StatisticsAppSuite

# Create core statistics library
dotnet new classlib -n Statistics.Core -o Statistics.Core

# Create client applications
dotnet new console -n Statistics.ConsoleApp -o Statistics.ConsoleApp
dotnet new blazorwasm -n Statistics.BlazorWasm -o Statistics.BlazorWasm
dotnet new maui -n Statistics.MauiApp -o Statistics.MauiApp
dotnet new maui-blazor -n Statistics.MauiBlazorApp -o Statistics.MauiBlazorApp

# Create server applications
dotnet new blazorserver -n Statistics.BlazorApp -o Statistics.BlazorApp
dotnet new webapi -n Statistics.Api -o Statistics.Api

# Add all projects to the solution
dotnet sln add Statistics.Core/Statistics.Core.csproj
dotnet sln add Statistics.ConsoleApp/Statistics.ConsoleApp.csproj
dotnet sln add Statistics.BlazorWasm/Statistics.BlazorWasm.csproj
dotnet sln add Statistics.BlazorApp/Statistics.BlazorApp.csproj
dotnet sln add Statistics.Api/Statistics.Api.csproj
dotnet sln add Statistics.MauiApp/Statistics.MauiApp.csproj
dotnet sln add Statistics.MauiBlazorApp/Statistics.MauiBlazorApp.csproj

# Update target framework in Statistics.Core for compatibility
# Edit Statistics.Core/Statistics.Core.csproj to set <TargetFramework>net6.0</TargetFramework>

# Add references to Statistics.Core from all client and server applications
dotnet add Statistics.ConsoleApp/Statistics.ConsoleApp.csproj reference Statistics.Core/Statistics.Core.csproj
dotnet add Statistics.BlazorWasm/Statistics.BlazorWasm.csproj reference Statistics.Core/Statistics.Core.csproj
dotnet add Statistics.BlazorApp/Statistics.BlazorApp.csproj reference Statistics.Core/Statistics.Core.csproj
dotnet add Statistics.Api/Statistics.Api.csproj reference Statistics.Core/Statistics.Core.csproj
dotnet add Statistics.MauiApp/Statistics.MauiApp.csproj reference Statistics.Core/Statistics.Core.csproj
dotnet add Statistics.MauiBlazorApp/Statistics.MauiBlazorApp.csproj reference Statistics.Core/Statistics.Core.csproj

# Restore packages and build the solution
dotnet restore
dotnet build

# Run individual applications (uncomment as needed)
# cd Statistics.ConsoleApp && dotnet run
# cd Statistics.Api && dotnet run
# cd Statistics.BlazorApp && dotnet run
# cd Statistics.BlazorWasm && dotnet run

# For MAUI applications (requires appropriate setup)
# cd Statistics.MauiApp && dotnet build -t:Run -f net6.0-android
# cd Statistics.MauiApp && dotnet build -t:Run -f net6.0-ios
# cd Statistics.MauiBlazorApp && dotnet build -t:Run -f net6.0-android
# cd Statistics.MauiBlazorApp && dotnet build -t:Run -f net6.0-ios