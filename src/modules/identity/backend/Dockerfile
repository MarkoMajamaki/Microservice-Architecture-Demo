FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 443

ENV ASPNETCORE_URLS=http://*:5000

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/modules/Identity/backend/Identity.Api/Identity.Api.csproj", "modules/Identity/backend/Identity.Api/"]
COPY ["src/modules/Identity/backend/Identity.Application/Identity.Application.csproj", "modules/Identity/backend/Identity.Application/"]
COPY ["src/modules/Identity/backend/Identity.Infrastructure/Identity.Infrastructure.csproj", "modules/Identity/backend/Identity.Infrastructure/"]
COPY ["src/modules/Identity/backend/Identity.Domain/Identity.Domain.csproj", "modules/Identity/backend/Identity.Domain/"]

COPY ["src/core/backend/Shared.Api/Shared.Api.csproj", "src/core/backend/Shared.Api/"]
COPY ["src/core/backend/Shared.Application/Shared.Application.csproj", "src/core/backend/Shared.Application/"]
COPY ["src/core/backend/Shared.Domain/Shared.Domain.csproj", "src/core/backend/Shared.Domain/"]
COPY ["src/core/backend/Shared.Infrastructure/Shared.Infrastructure.csproj", "src/core/backend/Shared.Infrastructure/"]

RUN dotnet restore "modules/Identity/backend/Identity.Api/Identity.Api.csproj"

COPY "src/modules/Identity/backend" "modules/Identity/backend/"
COPY "src/core/backend" "core/backend/"

WORKDIR "/src/modules/Identity/backend/Identity.Api/"
RUN dotnet build "Identity.Api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]