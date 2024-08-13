# Use the .NET 8.0 SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY *.sln .
COPY Gemini_Competition/*.csproj ./Gemini_Competition/
RUN dotnet restore "./Gemini_Competition/Gemini_Competition.csproj"

# Copy the remaining source code and build the app
COPY . ./Gemini_Competition/
WORKDIR /app/Gemini_Competition
RUN dotnet publish -c Release -o out

# Use the .NET 8.0 runtime for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/Gemini_Competition/out .

# Expose ports for HTTP and HTTPS (if applicable)
EXPOSE 80
EXPOSE 443

# Command to run the application
CMD ["dotnet", "Gemini_Competition.dll"]
