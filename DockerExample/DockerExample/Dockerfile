#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DockerExample/DockerExample.csproj", "DockerExample/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "DockerExample/DockerExample.csproj"
COPY . .
WORKDIR "/src/DockerExample"
RUN dotnet build "DockerExample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerExample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerExample.dll"]