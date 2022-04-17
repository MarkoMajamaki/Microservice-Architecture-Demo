FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/modules/basket/backend/Basket.Api/Basket.Api.csproj", "modules/basket/backend/Basket.Api/"]
COPY ["src/modules/basket/backend/Basket.Application/Basket.Application.csproj", "modules/basket/backend/Basket.Application/"]
COPY ["src/modules/basket/backend/Basket.Infrastructure/Basket.Infrastructure.csproj", "modules/basket/backend/Basket.Infrastructure/"]
COPY ["src/modules/basket/backend/Basket.Domain/Basket.Domain.csproj", "modules/basket/backend/Basket.Domain/"]

COPY ["src/shared/backend/Shared.Api/Shared.Api.csproj", "src/shared/backend/Shared.Api/"]
COPY ["src/shared/backend/Shared.Application/Shared.Application.csproj", "src/shared/backend/Shared.Application/"]
COPY ["src/shared/backend/Shared.Domain/Shared.Domain.csproj", "src/shared/backend/Shared.Domain/"]
COPY ["src/shared/backend/Shared.Infrastructure/Shared.Infrastructure.csproj", "src/shared/backend/Shared.Infrastructure/"]

RUN dotnet restore "modules/basket/backend/Basket.Api/Basket.Api.csproj"

COPY "src/modules/basket/backend" "modules/basket/backend/"
COPY "src/shared/backend" "shared/backend/"

WORKDIR "/src/modules/basket/backend/Basket.Api/"
RUN dotnet build "Basket.Api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Basket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]