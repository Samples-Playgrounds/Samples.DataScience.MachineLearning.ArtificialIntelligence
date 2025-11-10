#!/bin/bash

dotnet tool \
    uninstall \
        --global aspire.cli \
        --prerelease

dotnet tool \
    install \
        --global aspire.cli \
        --prerelease

dotnet new \
    uninstall \
        Aspire.ProjectTemplates
dotnet new \
    install \
        Aspire.ProjectTemplates

dotnet new \
    aspire \
        -o AppAspire.Yarp

cd AppAspire.Yarp

dotnet new \
    webapi \
        -o AppAspire.YarpProxy

dotnet sln \
    add \
        AppAspire.YarpProxy/AppAspire.YarpProxy.csproj \

dotnet add \
    AppAspire.YarpProxy/AppAspire.YarpProxy.csproj \
        package \
            Microsoft.ReverseProxy


dotnet new \
    blazor \
        -o AppBlazor.WebHomePage
dotnet sln \
    add \
        AppBlazor.WebHomePage/AppBlazor.WebHomePage.csproj
dotnet new \
    blazor \
        -o AppBlazor.WebPh4ct3x
dotnet sln \
    add \
        AppBlazor.WebPh4ct3x/AppBlazor.WebPh4ct3x.csproj




