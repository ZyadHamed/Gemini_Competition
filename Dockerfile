# Use the .NET 8.0 SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies as distinct layers
COPY *.sln .
COPY "Gemini Competition/*.csproj" "./Gemini Competition/"
RUN dotnet restore

# Copy everything else and build the app
COPY "Gemini Competition/." "./Gemini Competition/"
WORKDIR "/app/Gemini Competition"
RUN dotnet publish -c Release -o out

# Use the .NET 8.0 runtime for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build "/app/Gemini Competition/out" .

# Expose ports for HTTP and HTTPS (if applicable)
EXPOSE 80
EXPOSE 443

# Command to run the application
CMD ["dotnet", "Gemini Competition.dll"]
