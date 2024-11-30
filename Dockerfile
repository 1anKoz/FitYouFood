FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

RUN dotnet tool install --global dotnet-sonarscanner

ENV PATH="${PATH}:/root/.dotnet/tools"

COPY ["FitYouFood.sln", "./"]
COPY ["FitYouFood/FitYouFood.API.csproj", "FitYouFood/"]
COPY ["FitYouFood.Core/FitYouFood.Core.csproj", "FitYouFood.Core/"]
COPY ["FitYouFood.Infrastructure/FitYouFood.Infrastructure.csproj", "FitYouFood.Infrastructure/"]
COPY ["FitYouFood.Tests/FitYouFood.Tests.csproj", "FitYouFood.Tests/"]

RUN dotnet restore "FitYouFood/FitYouFood.API.csproj"
RUN dotnet restore "FitYouFood.Tests/FitYouFood.Tests.csproj"

COPY . .

WORKDIR "/src/FitYouFood"
RUN dotnet build "FitYouFood.API.csproj" -c Release -o /app/build

WORKDIR "/src/FitYouFood.Tests"
RUN dotnet build "FitYouFood.Tests.csproj" -c Debug -o /app/tests

RUN dotnet test "FitYouFood.Tests.csproj" --verbosity normal

WORKDIR "/src/FitYouFood"
RUN dotnet publish "FitYouFood.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

RUN dotnet tool install --global dotnet-sonarscanner

EXPOSE 80

ENTRYPOINT ["dotnet", "FitYouFood.API.dll"]
