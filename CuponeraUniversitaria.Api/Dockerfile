FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY CuponeraUniversitaria.Api/CuponeraUniversitaria.Api.csproj CuponeraUniversitaria.Api/
COPY CuponeraUniversitaria.Domain/CuponeraUniversitaria.Domain.csproj CuponeraUniversitaria.Domain/
COPY CuponeraUniversitaria.Infrastructure/CuponeraUniversitaria.Infrastructure.csproj CuponeraUniversitaria.Infrastructure/
RUN dotnet restore CuponeraUniversitaria.Api/CuponeraUniversitaria.Api.csproj
COPY . .
WORKDIR /src/CuponeraUniversitaria.Api
RUN dotnet build CuponeraUniversitaria.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish CuponeraUniversitaria.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CuponeraUniversitaria.Api.dll"]
