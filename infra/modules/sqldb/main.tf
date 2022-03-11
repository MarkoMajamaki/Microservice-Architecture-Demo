resource "azurerm_mssql_database" "db" {
  name           = var.name
  server_id      = var.sql_server_id
  max_size_gb    = var.size
  sku_name       = var.sku_name
  collation      = "SQL_Latin1_General_CP1_CI_AS"
  license_type   = "LicenseIncluded"
  read_scale     = var.read_scale
  zone_redundant = var.zone_redundant
}

resource "azurerm_mssql_database_extended_auditing_policy" "auditing_policy" {
  database_id                             = azurerm_mssql_database.db.id
  storage_endpoint                        = var.storage_endpoint
  storage_account_access_key              = var.storage_account_access_key
  storage_account_access_key_is_secondary = true
  retention_in_days                       = 6  
}