# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0.405-windowsservercore-ltsc2022 AS build
WORKDIR /app

# Copy the solution file and project files
COPY *.sln ./
COPY TournamentApi/TournamentApi.csproj TournamentApi/
COPY ServicesContracts/ServicesContracts.csproj ServicesContracts/
COPY TournamentCore/TournamentCore.csproj TournamentCore/
COPY TournamentData/TournamentData.csproj TournamentData/
COPY TournamentPresentation/TournamentPresentation.csproj TournamentPresentation/
COPY TournamentServices/TournamentServices.csproj TournamentServices/
COPY TournamentShared/TournamentShared.csproj TournamentShared/

# Restore dependencies for all projects
RUN dotnet restore

# Copy the rest of the code
COPY . .

# Build and publish the main app
RUN dotnet publish TournamentApi/TournamentApi.csproj -c Release -o out

# Use the official ASP.NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0.405-windowsservercore-ltsc2022
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "TournamentApi.dll"]
