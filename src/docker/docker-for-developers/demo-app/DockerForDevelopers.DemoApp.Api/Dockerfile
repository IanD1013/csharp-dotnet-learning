# Build context is src/, so the central Directory.Build.props and
# Directory.Packages.props are available to restore against.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "Directory.Packages.props", "./"]
COPY ["docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/DockerForDevelopers.DemoApp.Api.csproj", "docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/"]
RUN dotnet restore "docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/DockerForDevelopers.DemoApp.Api.csproj"
COPY docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/ docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/
WORKDIR "/src/docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api"
RUN dotnet publish "DockerForDevelopers.DemoApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DockerForDevelopers.DemoApp.Api.dll"]
