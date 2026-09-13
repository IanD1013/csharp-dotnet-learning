# Blazor WebAssembly publishes to static files, so the runtime stage is nginx
# rather than a .NET runtime image.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "Directory.Packages.props", "./"]
COPY ["docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Frontend/DockerForDevelopers.DemoApp.Frontend.csproj", "docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Frontend/"]
RUN dotnet restore "docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Frontend/DockerForDevelopers.DemoApp.Frontend.csproj"
COPY docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Frontend/ docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Frontend/
WORKDIR "/src/docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Frontend"
RUN dotnet publish "DockerForDevelopers.DemoApp.Frontend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
