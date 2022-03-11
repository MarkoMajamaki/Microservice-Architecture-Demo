FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/modules/identity/backend/Identity.Api/Identity.Api.csproj", "modules/identity/backend/Identity.Api/"]
COPY ["src/modules/identity/backend/Identity.Application/Identity.Application.csproj", "modules/identity/backend/Identity.Application/"]
COPY ["src/modules/identity/backend/Identity.Infrastructure/Identity.Infrastructure.csproj", "modules/identity/backend/Identity.Infrastructure/"]
COPY ["src/modules/identity/backend/Identity.Domain/Identity.Domain.csproj", "modules/Identity/backend/identity.Domain/"]

COPY ["src/shared/backend/Shared.Api/Shared.Api.csproj", "src/shared/backend/Shared.Api/"]
COPY ["src/shared/backend/Shared.Application/Shared.Application.csproj", "src/shared/backend/Shared.Application/"]
COPY ["src/shared/backend/Shared.Domain/Shared.Domain.csproj", "src/shared/backend/Shared.Domain/"]
COPY ["src/shared/backend/Shared.Infrastructure/Shared.Infrastructure.csproj", "src/shared/backend/Shared.Infrastructure/"]

RUN dotnet restore "modules/identity/backend/Identity.Api/Identity.Api.csproj"

COPY "src/modules/identity/backend" "modules/identity/backend/"
COPY "src/shared/backend" "shared/backend/"

WORKDIR "/src/modules/identity/backend/Identity.Api/"
RUN dotnet build "Identity.Api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]