#!/bin/bash

# Create solution file
dotnet new sln -n StatisticsSuite

# Create projects
dotnet new classlib -n StatisticsSuite.BusinessLogic -o StatisticsSuite.BusinessLogic
dotnet new maui -n StatisticsSuite.MauiApp -o StatisticsSuite.MauiApp
dotnet new maui-blazor -n StatisticsSuite.MauiBlazorApp -o StatisticsSuite.MauiBlazorApp
dotnet new console -n StatisticsSuite.ConsoleApp -o StatisticsSuite.ConsoleApp
dotnet new blazorwasm -n StatisticsSuite.BlazorWasmApp -o StatisticsSuite.BlazorWasmApp
dotnet new blazorserver -n StatisticsSuite.BlazorServerApp -o StatisticsSuite.BlazorServerApp
dotnet new webapi -n StatisticsSuite.WebApi -o StatisticsSuite.WebApi

# Add projects to solution
dotnet sln StatisticsSuite.sln add StatisticsSuite.BusinessLogic/StatisticsSuite.BusinessLogic.csproj \
    StatisticsSuite.MauiApp/StatisticsSuite.MauiApp.csproj \
    StatisticsSuite.MauiBlazorApp/StatisticsSuite.MauiBlazorApp.csproj \
    StatisticsSuite.ConsoleApp/StatisticsSuite.ConsoleApp.csproj \
    StatisticsSuite.BlazorWasmApp/StatisticsSuite.BlazorWasmApp.csproj \
    StatisticsSuite.BlazorServerApp/StatisticsSuite.BlazorServerApp.csproj \
    StatisticsSuite.WebApi/StatisticsSuite.WebApi.csproj

# Add references to BusinessLogic library
dotnet add StatisticsSuite.MauiApp/StatisticsSuite.MauiApp.csproj reference StatisticsSuite.BusinessLogic/StatisticsSuite.BusinessLogic.csproj
dotnet add StatisticsSuite.MauiBlazorApp/StatisticsSuite.MauiBlazorApp.csproj reference StatisticsSuite.BusinessLogic/StatisticsSuite.BusinessLogic.csproj
dotnet add StatisticsSuite.ConsoleApp/StatisticsSuite.ConsoleApp.csproj reference StatisticsSuite.BusinessLogic/StatisticsSuite.BusinessLogic.csproj
dotnet add StatisticsSuite.BlazorWasmApp/StatisticsSuite.BlazorWasmApp.csproj reference StatisticsSuite.BusinessLogic/StatisticsSuite.BusinessLogic.csproj
dotnet add StatisticsSuite.BlazorServerApp/StatisticsSuite.BlazorServerApp.csproj reference StatisticsSuite.BusinessLogic/StatisticsSuite.BusinessLogic.csproj
dotnet add StatisticsSuite.WebApi/StatisticsSuite.WebApi.csproj reference StatisticsSuite.BusinessLogic/StatisticsSuite.BusinessLogic.csproj

echo "Setup complete."
