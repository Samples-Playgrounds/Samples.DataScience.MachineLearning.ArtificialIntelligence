#!/bin/bash
# Commands to set up the Statistics Suite solution and projects

# Create solution file
dotnet new sln -n StatisticsSuite

# Create Statistics Library
dotnet new classlib -n StatisticsLibrary -o StatisticsLibrary
dotnet sln StatisticsSuite.sln add StatisticsLibrary/StatisticsLibrary.csproj

# Create MAUI App
dotnet new maui -n StatisticsMauiApp -o StatisticsMauiApp
dotnet sln StatisticsSuite.sln add StatisticsMauiApp/StatisticsMauiApp.csproj
dotnet add StatisticsMauiApp/StatisticsMauiApp.csproj reference StatisticsLibrary/StatisticsLibrary.csproj

# Create Blazor Hybrid (MAUI) App
dotnet new maui-blazor -n StatisticsMauiBlazorApp -o StatisticsMauiBlazorApp
dotnet sln StatisticsSuite.sln add StatisticsMauiBlazorApp/StatisticsMauiBlazorApp.csproj
dotnet add StatisticsMauiBlazorApp/StatisticsMauiBlazorApp.csproj reference StatisticsLibrary/StatisticsLibrary.csproj

# Create Console App
dotnet new console -n StatisticsConsoleApp -o StatisticsConsoleApp
dotnet sln StatisticsSuite.sln add StatisticsConsoleApp/StatisticsConsoleApp.csproj
dotnet add StatisticsConsoleApp/StatisticsConsoleApp.csproj reference StatisticsLibrary/StatisticsLibrary.csproj

# Create Blazor WASM App
dotnet new blazorwasm -n StatisticsBlazorWasmApp -o StatisticsBlazorWasmApp
dotnet sln StatisticsSuite.sln add StatisticsBlazorWasmApp/StatisticsBlazorWasmApp.csproj
dotnet add StatisticsBlazorWasmApp/StatisticsBlazorWasmApp.csproj reference StatisticsLibrary/StatisticsLibrary.csproj

# Create Blazor Server App
dotnet new blazor -n StatisticsBlazorServerApp -o StatisticsBlazorServerApp --interactivity Server --empty
dotnet sln StatisticsSuite.sln add StatisticsBlazorServerApp/StatisticsBlazorServerApp.csproj
dotnet add StatisticsBlazorServerApp/StatisticsBlazorServerApp.csproj reference StatisticsLibrary/StatisticsLibrary.csproj

# Create Web API
dotnet new webapi -n StatisticsWebApi -o StatisticsWebApi
dotnet sln StatisticsSuite.sln add StatisticsWebApi/StatisticsWebApi.csproj
dotnet add StatisticsWebApi/StatisticsWebApi.csproj reference StatisticsLibrary/StatisticsLibrary.csproj

# Optional: Build the solution
# dotnet build StatisticsSuite.sln

echo "Project setup complete."
