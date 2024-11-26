# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj and restore as distinct layers for better caching
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining source code
COPY . ./

# Publish the application
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Set the user to a non-root user if available
USER app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Expose necessary ports
EXPOSE 8080
EXPOSE 8081

# Set the entry point for the container
ENTRYPOINT ["dotnet", "AimReactionAPI.dll"]
