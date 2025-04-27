#!/bin/bash

# Create solution
dotnet new sln -n StatisticsSuite

# Create class library
dotnet new classlib -n Statistics.Library -o Statistics.Library
dotnet sln StatisticsSuite.sln add Statistics.Library/Statistics.Library.csproj

# Create MAUI App
dotnet new maui -n Statistics.MAUI.App -o Statistics.MAUI.App
dotnet sln StatisticsSuite.sln add Statistics.MAUI.App/Statistics.MAUI.App.csproj
dotnet add Statistics.MAUI.App/Statistics.MAUI.App.csproj reference Statistics.Library/Statistics.Library.csproj

# Create MAUI Blazor App
dotnet new maui-blazor -n Statistics.MAUI.Blazor.App -o Statistics.MAUI.Blazor.App
dotnet sln StatisticsSuite.sln add Statistics.MAUI.Blazor.App/Statistics.MAUI.Blazor.App.csproj
dotnet add Statistics.MAUI.Blazor.App/Statistics.MAUI.Blazor.App.csproj reference Statistics.Library/Statistics.Library.csproj

# Create Console App
dotnet new console -n Statistics.Console.App -o Statistics.Console.App
dotnet sln StatisticsSuite.sln add Statistics.Console.App/Statistics.Console.App.csproj
dotnet add Statistics.Console.App/Statistics.Console.App.csproj reference Statistics.Library/Statistics.Library.csproj

# Create Blazor WASM App
dotnet new blazorwasm -n Statistics.Blazor.WASM.App -o Statistics.Blazor.WASM.App
dotnet sln StatisticsSuite.sln add Statistics.Blazor.WASM.App/Statistics.Blazor.WASM.App.csproj
dotnet add Statistics.Blazor.WASM.App/Statistics.Blazor.WASM.App.csproj reference Statistics.Library/Statistics.Library.csproj

# Create Blazor Server App
dotnet new blazor -n Statistics.Blazor.Server.App -o Statistics.Blazor.Server.App
dotnet sln StatisticsSuite.sln add Statistics.Blazor.Server.App/Statistics.Blazor.Server.App.csproj
dotnet add Statistics.Blazor.Server.App/Statistics.Blazor.Server.App.csproj reference Statistics.Library/Statistics.Library.csproj

# Create Web API
dotnet new webapi -n Statistics.Web.API -o Statistics.Web.API
dotnet sln StatisticsSuite.sln add Statistics.Web.API/Statistics.Web.API.csproj
dotnet add Statistics.Web.API/Statistics.Web.API.csproj reference Statistics.Library/Statistics.Library.csproj

echo "Setup complete."
