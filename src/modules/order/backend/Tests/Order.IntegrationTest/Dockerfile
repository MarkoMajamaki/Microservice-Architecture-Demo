#
# Docker file which runs integration tests inside container
#
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /src
COPY ["src/modules/order/backend/Order.Api/Order.Api.csproj", "modules/order/backend/Order.Api/"]
COPY ["src/modules/order/backend/Order.Application/Order.Application.csproj", "modules/order/backend/Order.Application/"]
COPY ["src/modules/order/backend/Order.Infrastructure/Order.Infrastructure.csproj", "modules/order/backend/Order.Infrastructure/"]
COPY ["src/modules/order/backend/Order.Domain/Order.Domain.csproj", "modules/order/backend/Order.Domain/"]

COPY ["src/core/backend/Shared.Api/Shared.Api.csproj", "src/core/backend/Shared.Api/"]
COPY ["src/core/backend/Shared.Application/Shared.Application.csproj", "src/core/backend/Shared.Application/"]
COPY ["src/core/backend/Shared.Domain/Shared.Domain.csproj", "src/core/backend/Shared.Domain/"]
COPY ["src/core/backend/Shared.Infrastructure/Shared.Infrastructure.csproj", "src/core/backend/Shared.Infrastructure/"]

COPY ["src/modules/order/backend/Tests/Order.IntegrationTest/Order.IntegrationTest.csproj", "modules/order/backend/Tests/Order.IntegrationTest/"]

RUN dotnet restore "modules/order/backend/Tests/Order.IntegrationTest/Order.IntegrationTest.csproj"

COPY "src/modules/order/backend" "modules/order/backend/"
COPY "src/core/backend" "core/backend/"

WORKDIR "/src/modules/order/backend/Tests/Order.IntegrationTest/"
RUN dotnet build "Order.IntegrationTest.csproj" -c Debug 
CMD dotnet test --logger trx --results-directory /var/temp