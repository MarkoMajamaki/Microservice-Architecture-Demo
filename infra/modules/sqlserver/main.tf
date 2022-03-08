resource "azurerm_mssql_server" "sql_server" {
  name                         = var.name
  resource_group_name          = var.rg_name
  location                     = var.location
  version                      = var.server_version
  administrator_login          = var.username
  administrator_login_password = var.password
  minimum_tls_version          = "1.2"
}

resource "azurerm_storage_account" "storage_account" {
  name                     = var.storage_account_name
  resource_group_name      = var.rg_name
  location                 = var.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}