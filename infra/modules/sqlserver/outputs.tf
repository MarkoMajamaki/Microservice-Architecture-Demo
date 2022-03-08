output "id" {
    value = azurerm_mssql_server.sql_server.id
}

output "storage_endpoint" {
  value = azurerm_storage_account.storage_account.primary_blob_endpoint
}

output "storage_account_access_key" {
  value = azurerm_storage_account.storage_account.primary_access_key
}