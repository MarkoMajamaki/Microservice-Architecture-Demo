FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 443

ENV ASPNETCORE_URLS=http://*:5000

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/modules/order/backend/Order.Api/Order.Api.csproj", "modules/order/backend/Order.Api/"]
COPY ["src/modules/order/backend/Order.Application/Order.Application.csproj", "modules/order/backend/Order.Application/"]
COPY ["src/modules/order/backend/Order.Infrastructure/Order.Infrastructure.csproj", "modules/order/backend/Order.Infrastructure/"]
COPY ["src/modules/order/backend/Order.Domain/Order.Domain.csproj", "modules/order/backend/Order.Domain/"]

COPY ["src/core/backend/Shared.Api/Shared.Api.csproj", "src/core/backend/Shared.Api/"]
COPY ["src/core/backend/Shared.Application/Shared.Application.csproj", "src/core/backend/Shared.Application/"]
COPY ["src/core/backend/Shared.Domain/Shared.Domain.csproj", "src/core/backend/Shared.Domain/"]
COPY ["src/core/backend/Shared.Infrastructure/Shared.Infrastructure.csproj", "src/core/backend/Shared.Infrastructure/"]

RUN dotnet restore "modules/order/backend/Order.Api/Order.Api.csproj"

COPY "src/modules/order/backend" "modules/order/backend/"
COPY "src/core/backend" "core/backend/"

WORKDIR "/src/modules/order/backend/Order.Api/"
RUN dotnet build "Order.Api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Order.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Api.dll"]