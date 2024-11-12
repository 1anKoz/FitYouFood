# Use the .NET SDK image for building the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and project files
COPY ["FitYouFood.sln", "./"]
COPY ["FitYouFood/FitYouFood.API.csproj", "FitYouFood/"]
COPY ["FitYouFood.Core/FitYouFood.Core.csproj", "FitYouFood.Core/"]
COPY ["FitYouFood.Infrastructure/FitYouFood.Infrastructure.csproj", "FitYouFood.Infrastructure/"]

# Restore NuGet packages for all projects
RUN dotnet restore "FitYouFood/FitYouFood.API.csproj"

# Copy the rest of the files
COPY . .

# Build and publish the application (specifically FitYouFood.API)
WORKDIR "/src/FitYouFood"
RUN dotnet build "FitYouFood.API.csproj" -c Release -o /app/build
RUN dotnet publish "FitYouFood.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the ASP.NET runtime image to create the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port the app will run on
EXPOSE 80

# Set the entry point to start the API
ENTRYPOINT ["dotnet", "FitYouFood.API.dll"]
