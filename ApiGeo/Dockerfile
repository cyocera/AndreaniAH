#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ApiGeo/ApiGeo.csproj", "ApiGeo/"]
RUN dotnet restore "ApiGeo/ApiGeo.csproj"
COPY . .
WORKDIR "/src/ApiGeo"
RUN dotnet build "ApiGeo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGeo.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGeo.dll"]