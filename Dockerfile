# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and project files first
COPY ["FitYouFood.sln", "./"]
COPY ["FitYouFood/FitYouFood.API.csproj", "FitYouFood/"]
COPY ["FitYouFood.Core/FitYouFood.Core.csproj", "FitYouFood.Core/"]
COPY ["FitYouFood.Infrastructure/FitYouFood.Infrastructure.csproj", "FitYouFood.Infrastructure/"]
COPY ["FitYouFood.Tests/FitYouFood.Tests.csproj", "FitYouFood.Tests/"]

# Run restore so all dependencies (including prometheus) are restored
RUN dotnet restore "FitYouFood/FitYouFood.API.csproj"
RUN dotnet restore "FitYouFood.Tests/FitYouFood.Tests.csproj"

# Now copy the rest of the application code
COPY . .

# Add the prometheus package
RUN dotnet add FitYouFood/FitYouFood.API.csproj package prometheus-net.AspNetCore

# Build the project
WORKDIR "/src/FitYouFood"
RUN dotnet build "FitYouFood.API.csproj" -c Release -o /app/build

# Build and run tests
WORKDIR "/src/FitYouFood.Tests"
RUN dotnet build "FitYouFood.Tests.csproj" -c Debug -o /app/tests
RUN dotnet test "FitYouFood.Tests.csproj" --verbosity normal

# Publish the project
WORKDIR "/src/FitYouFood"
RUN dotnet publish "FitYouFood.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app

# Copy the published files from the build image
COPY --from=build /app/publish ./

# Copy the .csproj files to the final image (optional)
COPY --from=build /src/FitYouFood/FitYouFood.API.csproj ./FitYouFood/
COPY --from=build /src/FitYouFood.Core/FitYouFood.Core.csproj ./FitYouFood.Core/
COPY --from=build /src/FitYouFood.Infrastructure/FitYouFood.Infrastructure.csproj ./FitYouFood.Infrastructure/
COPY --from=build /src/FitYouFood.Tests/FitYouFood.Tests.csproj ./FitYouFood.Tests/

# Install dotnet-sonarscanner tool
RUN dotnet tool install --global dotnet-sonarscanner

# Expose port for the application
EXPOSE 80

# Set entrypoint to run the API
ENTRYPOINT ["dotnet", "FitYouFood.API.dll"]
