# Basic
location="northeurope"
rg_name="microservice-demo-beta-rg"
acr_name="microservicedemoacrbeta"
kv_name="microservicedemokvbeta"
# AKS
cluster_name="microservicedemoaksbeta"
kubernetes_version="1.21.2"
node_count=1
aks_vm="Standard_A4_v2"
# Database
sql_server_name="m-sqlserver-beta"
sql_server_username="microservicedemouser"
sql_server_password="4-v3ry-53cr37-p455w0rd"
order_db_name="order-db-beta"
identity_db_name="identity-db-beta"
sql_server_storage_account_name="sqlserversabeta"
# App Service
app_service_name="app-service-beta"
service_plan_name="app-service-plan-beta"
service_plan_tier="Basic"
service_plan_size="B1"