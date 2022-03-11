FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/modules/order/backend/Order.Api/Order.Api.csproj", "modules/order/backend/Order.Api/"]
COPY ["src/modules/order/backend/Order.Application/Order.Application.csproj", "modules/order/backend/Order.Application/"]
COPY ["src/modules/order/backend/Order.Infrastructure/Order.Infrastructure.csproj", "modules/order/backend/Order.Infrastructure/"]
COPY ["src/modules/order/backend/Order.Domain/Order.Domain.csproj", "modules/order/backend/Order.Domain/"]

COPY ["src/shared/backend/Shared.Api/Shared.Api.csproj", "src/shared/backend/Shared.Api/"]
COPY ["src/shared/backend/Shared.Application/Shared.Application.csproj", "src/shared/backend/Shared.Application/"]
COPY ["src/shared/backend/Shared.Domain/Shared.Domain.csproj", "src/shared/backend/Shared.Domain/"]
COPY ["src/shared/backend/Shared.Infrastructure/Shared.Infrastructure.csproj", "src/shared/backend/Shared.Infrastructure/"]

RUN dotnet restore "modules/order/backend/Order.Api/Order.Api.csproj"

COPY "src/modules/order/backend" "modules/order/backend/"
COPY "src/shared/backend" "shared/backend/"

WORKDIR "/src/modules/order/backend/Order.Api/"
RUN dotnet build "Order.Api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Order.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Api.dll"]