FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["FitYouFood.sln", "./"]
COPY ["FitYouFood/FitYouFood.API.csproj", "FitYouFood/"]
COPY ["FitYouFood.Core/FitYouFood.Core.csproj", "FitYouFood.Core/"]
COPY ["FitYouFood.Infrastructure/FitYouFood.Infrastructure.csproj", "FitYouFood.Infrastructure/"]
COPY ["FitYouFood.Tests/FitYouFood.Tests.csproj", "FitYouFood.Tests/"]

RUN dotnet restore "FitYouFood/FitYouFood.API.csproj"
RUN dotnet restore "FitYouFood.Tests/FitYouFood.Tests.csproj"

COPY . .

RUN dotnet add FitYouFood/FitYouFood.API.csproj package prometheus-net.AspNetCore

WORKDIR "/src/FitYouFood"
RUN dotnet build "FitYouFood.API.csproj" -c Release -o /app/build

WORKDIR "/src/FitYouFood.Tests"
RUN dotnet build "FitYouFood.Tests.csproj" -c Debug -o /app/tests
RUN dotnet test "FitYouFood.Tests.csproj" --verbosity normal

WORKDIR "/src/FitYouFood"
RUN dotnet publish "FitYouFood.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish ./

COPY --from=build /src/FitYouFood/ ./FitYouFood/
COPY --from=build /src/FitYouFood.Core/ ./FitYouFood.Core/
COPY --from=build /src/FitYouFood.Infrastructure/ ./FitYouFood.Infrastructure/
COPY --from=build /src/FitYouFood.Tests/ ./FitYouFood.Tests/

RUN dotnet tool install --global dotnet-sonarscanner

ENV PATH="${PATH}:/root/.dotnet/tools"

EXPOSE 80

ENTRYPOINT ["dotnet", "FitYouFood.API.dll"]