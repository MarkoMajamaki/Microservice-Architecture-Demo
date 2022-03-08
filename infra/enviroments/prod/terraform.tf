terraform {
  required_providers {
    azurerm = {
      version = "=2.86.0"
    }
  }
  backend "azurerm" {
    resource_group_name   = "microservice_demo_tfstate_rg"
    storage_account_name  = "microservicedemosa"
    container_name        = "microservice-demo-tfstate-c"
    key                   = "prod.terraform.tfstate"
  }
}