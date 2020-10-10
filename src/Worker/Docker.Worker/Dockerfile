#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Worker/Docker.Worker/Docker.Worker.csproj", "src/Worker/Docker.Worker/"]
COPY ["src/Shared/Docker.Data/Docker.Data.csproj", "src/Shared/Docker.Data/"]
COPY ["src/Shared/Docker.Core/Docker.Core.csproj", "src/Shared/Docker.Core/"]
RUN dotnet restore "src/Worker/Docker.Worker/Docker.Worker.csproj"
COPY . .
WORKDIR "/src/src/Worker/Docker.Worker"
RUN dotnet build "Docker.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Docker.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Docker.Worker.dll"]