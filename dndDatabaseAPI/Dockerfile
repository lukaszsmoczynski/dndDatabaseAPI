#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["dndDatabaseAPI/dndDatabaseAPI.csproj", "dndDatabaseAPI/"]
RUN dotnet restore "dndDatabaseAPI/dndDatabaseAPI.csproj"
COPY . .
WORKDIR "/src/dndDatabaseAPI"
RUN dotnet publish "dndDatabaseAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "dndDatabaseAPI.dll"]