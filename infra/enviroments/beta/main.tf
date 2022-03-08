provider "azurerm" {
  features {}
}

module "rg" {
  source = "./../../modules/rg"
  name = var.rg_name
  location = var.location
}

module "acr" {
  source = "./../../modules/acr"
  name = var.acr_name
  rg_name = module.rg.name
  location = var.location
  sku = "Standard"

  depends_on = [
    module.rg
  ]
}

module "aks" {
  source = "./../../modules/aks"
  acr_id = module.acr.acr_id
  cluster_name = var.cluster_name
  kubernetes_version = var.kubernetes_version
  location = var.location
  rg_name = module.rg.name
  node_count = var.node_count
  vm = var.vm
}

module "kv" {
  source = "./../../modules/kv"
  name = var.kv_name
  rg_name = module.rg.name
  location = var.location
}

module "sql_server" {
  source = "./../../modules/sqlserver"
  name = var.sql_server_name
  rg_name = module.rg.name
  location = var.location
  username = var.sql_server_username
  password = var.sql_server_password
  storage_account_name = var.sql_server_storage_account_name
  server_version = "12.0"
}

module "identity_db" {
  source = "./../../modules/sqldb"
  name = var.identity_db_name
  sql_server_id = module.sql_server.id
  storage_endpoint = module.sql_server.storage_endpoint
  storage_account_access_key = module.sql_server.storage_account_access_key
  sku_name = "Basic"
  size = 2
}

module "order_db" {
  source = "./../../modules/sqldb"
  name = var.order_db_name
  sql_server_id = module.sql_server.id
  storage_endpoint = module.sql_server.storage_endpoint
  storage_account_access_key = module.sql_server.storage_account_access_key
  sku_name = "Basic"
  size = 2
}