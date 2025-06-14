# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY PubSubApp.sln .
COPY PubSubAPI/ ./PubSubAPI/
COPY PubSubCore/ ./PubSubCore/
COPY PubSubApp/ ./PubSubApp/

# Restore dependencies
RUN dotnet restore PubSubApp.sln

# Publish the application
WORKDIR /src/PubSubAPI
RUN dotnet publish "PubSubAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Serve the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PubSubAPI.dll"]
