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
    always_on        = true
    min_tls_version  = 1.2
    ftps_state       = "Disabled"
    linux_fx_version = "DOCKER|${var.image_name}"
  }

  logs {
    http_logs {
      file_system {
        retention_in_days = 90
        retention_in_mb   = 50
      }
    }
  }
}