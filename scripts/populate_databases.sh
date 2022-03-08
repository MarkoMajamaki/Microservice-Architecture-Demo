# Build Order module
dotnet build src/modules/order/backend/Tools/Order.DbPopulator/

# Run Order module database populator
dotnet run \
    --project src/modules/order/backend/Tools/Order.DbPopulator/Order.DbPopulator.csproj -- \
    -ip localhost \
    -port 1433 \
    -u sa \
    -n order-db \
    -p loc4LdevP4ss#

# Build Identity module
dotnet build src/modules/Identity/backend/Tools/Identity.DbPopulator/

# Run Identity module database populator
dotnet run \
    --project src/modules/Identity/backend/Tools/Identity.DbPopulator/Identity.DbPopulator.csproj -- \
    -ip localhost \
    -port 1433 \
    -u sa \
    -n identity-db \
    -p loc4LdevP4ss#