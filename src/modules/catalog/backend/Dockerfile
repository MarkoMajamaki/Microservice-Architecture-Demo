FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/modules/catalog/backend/Catalog.Api/Catalog.Api.csproj", "modules/catalog/backend/Catalog.Api/"]
COPY ["src/modules/catalog/backend/Catalog.Application/Catalog.Application.csproj", "modules/catalog/backend/Catalog.Application/"]
COPY ["src/modules/catalog/backend/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "modules/catalog/backend/Catalog.Infrastructure/"]
COPY ["src/modules/catalog/backend/Catalog.Domain/Catalog.Domain.csproj", "modules/catalog/backend/Catalog.Domain/"]

COPY ["src/shared/backend/Shared.Api/Shared.Api.csproj", "src/shared/backend/Shared.Api/"]
COPY ["src/shared/backend/Shared.Application/Shared.Application.csproj", "src/shared/backend/Shared.Application/"]
COPY ["src/shared/backend/Shared.Domain/Shared.Domain.csproj", "src/shared/backend/Shared.Domain/"]
COPY ["src/shared/backend/Shared.Infrastructure/Shared.Infrastructure.csproj", "src/shared/backend/Shared.Infrastructure/"]

RUN dotnet restore "modules/catalog/backend/Catalog.Api/Catalog.Api.csproj"

COPY "src/modules/catalog/backend" "modules/catalog/backend/"
COPY "src/shared/backend" "shared/backend/"

WORKDIR "/src/modules/catalog/backend/Catalog.Api/"
RUN dotnet build "Catalog.Api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Catalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Api.dll"]