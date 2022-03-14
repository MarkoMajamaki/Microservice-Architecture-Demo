resource "azurerm_container_group" "aci" {
  name                = var.name
  location            = var.location
  resource_group_name = var.rg_name
  ip_address_type     = "public"
  os_type             = "Linux"

  image_registry_credential {
    username = var.acr_username
    password = var.acr_password
    server =  var.acr_server
  }

	#
  # Set environment variables etc!
	# TODO: Ports should be unique for each container!
	#

  container {
    name   = "gateway"
    image  = "envoyproxy/envoy:v1.20.2"
    cpu    = "0.5"
    memory = "0.2"

    ports {
      port     = 80
      protocol = "TCP"
    }
  }

  container {
    name   = "frontend"
    image  = "microservicedemoacrbeta.azurecr.io/frontend:latest"
    cpu    = "0.5"
    memory = "0.2"
  }

  container {
    name   = "orderapi"
    image  = "microservicedemoacrbeta.azurecr.io/order-api:latest"
    cpu    = "0.5"
    memory = "0.2"
  }

  container {
    name   = "identityapi"
    image  = "microservicedemoacrbeta.azurecr.io/identity-api:latest"
    cpu    = "0.5"
    memory = "0.2"
  }

  container {
    name   = "rabbitmq"
    image  = "rabbitmq:3-management"
    cpu    = "0.5"
    memory = "0.2"
  }
}