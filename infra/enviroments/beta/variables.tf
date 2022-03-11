variable "location" {
  type = string
}

variable "rg_name" {
  type = string
}

variable "acr_name" {
  type = string
}

variable "cluster_name" {
  type = string 
}

variable "kubernetes_version" {
  type = string 
}

variable "node_count" {
  type = number 
}

variable "aks_vm" {
  type = string 
}

variable "kv_name" {
  type = string
}

variable "sql_server_name" {
  type = string
}

variable "sql_server_username" {
  type = string  
}

variable "sql_server_password" {
  type = string  
}

variable "order_db_name" {
  type = string    
}

variable "identity_db_name" {
  type = string    
}

variable "sql_server_storage_account_name" {
  type = string
}

variable "service_plan_name" {
  type = string
}

variable "service_plan_tier" {
  type    = string
}

variable "service_plan_size" {
  type    = string
}

variable "app_service_name" {
  type = string
}