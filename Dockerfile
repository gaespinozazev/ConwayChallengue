# Use the official .NET 7 SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /app

# Copy csproj files and restore dependencies
COPY ["Game.API/Game.API.csproj", "Game.API/"]
COPY ["Game.Application/Game.Application.csproj", "Game.Application/"]
COPY ["Game.Domain/Game.Domain.csproj", "Game.Domain/"]
COPY ["Game.Infra.Data/Game.Infra.Data.csproj", "Game.Infra.Data/"]

RUN dotnet restore "Game.API/Game.API.csproj"

# Copy the rest of the application code
COPY . .

# Publish the application
RUN dotnet publish "Game.API/Game.API.csproj" -c Release -o /app/out

# Use the official .NET 7 runtime image as a runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose port 80
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Game.API.dll"]
