# Build order module
dotnet build src/modules/order/backend/Tools/Order.Migrations/

# Run order module migrations
dotnet run \
    --project src/modules/order/backend/Tools/Order.Migrations/Order.Migrations.csproj -- \
    -ip localhost \
    -port 1433 \
    -u sa \
    -n order-db \
    -p loc4LdevP4ss#

# Build identity module
dotnet build src/modules/Identity/backend/Tools/Identity.Migrations/

# Run identity module migrations
dotnet run \
    --project src/modules/Identity/backend/Tools/Identity.Migrations/Identity.Migrations.csproj -- \
    -ip localhost \
    -port 1433 \
    -u sa \
    -n identity-db \
    -p loc4LdevP4ss#