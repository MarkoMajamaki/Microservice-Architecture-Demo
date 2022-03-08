data "azurerm_client_config" "current" {}

resource "azurerm_key_vault" "kv" {
  name = var.name
  location =  var.location
  resource_group_name = var.rg_name
  enabled_for_disk_encryption = true
  tenant_id = data.azurerm_client_config.current.tenant_id
  purge_protection_enabled = false
  sku_name = "standard"
}

resource "azurerm_key_vault_access_policy" "kv_access_policy" {
  key_vault_id = azurerm_key_vault.kv.id
  tenant_id    = data.azurerm_client_config.current.tenant_id
  object_id    = data.azurerm_client_config.current.object_id

  key_permissions = [
    "Get",
  ]

  secret_permissions = [
    "Get",
    "List",
    "Set",
  ]
}