FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Install dotnet-sonarscanner
RUN dotnet tool install --global dotnet-sonarscanner

# Ensure the tool is in the PATH
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY ["FitYouFood.sln", "./"]
COPY ["FitYouFood/FitYouFood.API.csproj", "FitYouFood/"]
COPY ["FitYouFood.Core/FitYouFood.Core.csproj", "FitYouFood.Core/"]
COPY ["FitYouFood.Infrastructure/FitYouFood.Infrastructure.csproj", "FitYouFood.Infrastructure/"]



RUN dotnet restore "FitYouFood/FitYouFood.API.csproj"

COPY . .

WORKDIR "/src/FitYouFood"
RUN dotnet build "FitYouFood.API.csproj" -c Release -o /app/build
RUN dotnet publish "FitYouFood.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-sonarscanner

EXPOSE 80

ENTRYPOINT ["dotnet", "FitYouFood.API.dll"]
