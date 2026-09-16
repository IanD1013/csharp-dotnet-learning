# Build context is src/, so the central Directory.Build.props and
# Directory.Packages.props are available to restore against.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "Directory.Packages.props", ".editorconfig", "./"]
COPY ["docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/DockerForDevelopers.DemoApp.Api.csproj", "docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/"]
COPY ["docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Tests/DockerForDevelopers.DemoApp.Tests.csproj", "docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Tests/"]
RUN dotnet restore "docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Tests/DockerForDevelopers.DemoApp.Tests.csproj"
COPY docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/ docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/
COPY docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Tests/ docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Tests/
WORKDIR "/src/docker/docker-for-developers/demo-app"
RUN dotnet build "DockerForDevelopers.DemoApp.Api/DockerForDevelopers.DemoApp.Api.csproj" -c Release
# The tests hit a real database at localhost:1433, so this layer only succeeds when the
# build is given the host network: docker build --network host ...
RUN dotnet test "DockerForDevelopers.DemoApp.Tests/DockerForDevelopers.DemoApp.Tests.csproj" -c Release
RUN dotnet publish "DockerForDevelopers.DemoApp.Api/DockerForDevelopers.DemoApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DockerForDevelopers.DemoApp.Api.dll"]
