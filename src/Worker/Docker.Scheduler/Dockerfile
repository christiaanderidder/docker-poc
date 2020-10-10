#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Worker/Docker.Scheduler/Docker.Scheduler.csproj", "src/Worker/Docker.Scheduler/"]
COPY ["src/Shared/Docker.Data/Docker.Data.csproj", "src/Shared/Docker.Data/"]
COPY ["src/Shared/Docker.Core/Docker.Core.csproj", "src/Shared/Docker.Core/"]
RUN dotnet restore "src/Worker/Docker.Scheduler/Docker.Scheduler.csproj"
COPY . .
WORKDIR "/src/src/Worker/Docker.Scheduler"
RUN dotnet build "Docker.Scheduler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Docker.Scheduler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Docker.Scheduler.dll"]