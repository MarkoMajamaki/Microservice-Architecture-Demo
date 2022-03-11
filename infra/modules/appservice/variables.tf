variable "location" {
  type = string
}

variable "rg_name" {
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

variable "image_name" {
  type = string
}

variable "app_settings" {
  type = map
}