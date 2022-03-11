# Basic
location="northeurope"
rg_name="microservice-demo-prod-rg"
acr_name="microservicedemoacrprod"
kv_name="microservicedemokvprod"
# AKS
cluster_name="microservicedemoaksprod"
kubernetes_version="1.21.2"
node_count=1
aks_vm="Standard_A4_v2"
# Database
sql_server_name="sqlserver-prod"
sql_server_username="microservicedemouser"
sql_server_password="4-v3ry-53cr37-p455w0rd"
order_db_name="order-db-prod"
identity_db_name="identity-db-prod"
sql_server_storage_account_name="sqlserversaprod"
# App Service
app_service_name="app-service-prod"
service_plan_name="app-service-plan-prod"
service_plan_tier="Basic"
service_plan_size="B1"