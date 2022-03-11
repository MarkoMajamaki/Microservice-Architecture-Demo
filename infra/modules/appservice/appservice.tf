# Service plan 
resource "azurerm_app_service_plan" "plan" {
  name                = var.service_plan_name
  location            = var.location
  resource_group_name = var.rg_name
  kind                = "Linux"
  reserved            = true # must be true for Linux

  sku {
    tier = var.service_plan_tier
    size = var.service_plan_size
  }
}

# App service
resource "azurerm_app_service" "app" {
  name                    = var.app_service_name
  location                = var.location
  resource_group_name     = var.rg_name
  app_service_plan_id     = azurerm_app_service_plan.plan.id
  https_only              = true
  client_affinity_enabled = false
  app_settings            = var.app_settings

  site_config {
    ftps_state       = "Disabled"
    app_command_line = ""
    linux_fx_version = "COMPOSE|${var.docker_compose}"
  }
}