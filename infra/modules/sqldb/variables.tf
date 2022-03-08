variable "name" {
  type = string
}

variable "sql_server_id" {
  type = string
}

variable "size" {
  type = number
}

variable "sku_name" {
  type = string
}

variable "storage_endpoint" {
  type = string
}

variable "storage_account_access_key" {
  type = string
}

variable "read_scale" {
  type = bool
  default = false
}

variable "zone_redundant" {
  type = bool
  default = false
}