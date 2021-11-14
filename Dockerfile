FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RateLimitApp.csproj", "./"]
RUN dotnet restore "RateLimitApp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RateLimitApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RateLimitApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RateLimitApp.dll"]
