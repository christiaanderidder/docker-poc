#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Web/Docker.Web/Docker.Web.csproj", "src/Web/Docker.Web/"]
COPY ["src/Shared/Docker.Data/Docker.Data.csproj", "src/Shared/Docker.Data/"]
COPY ["src/Shared/Docker.Core/Docker.Core.csproj", "src/Shared/Docker.Core/"]
RUN dotnet restore "src/Web/Docker.Web/Docker.Web.csproj"
COPY . .
WORKDIR "/src/src/Web/Docker.Web"
RUN dotnet build "Docker.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Docker.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Docker.Web.dll"]