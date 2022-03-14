# Deploy to Azure Kubernetes Services

## Deploy
```bash
# Set enviroment to prod or beta
env=beta

cluster_name=microservicedemoaks$env
resource_group_name=microservice-demo-$env-rg
key_vault_name=microservicedemokv$env

echo "Deploy $env enviroment"

# Create Terraform state storage
sh infra/create_tfstate_storage.sh

# Init infrastructure with Terraform, do plan, and apply plan to create base infrastructure
terraform -chdir=infra/enviroments/$env init
terraform -chdir=infra/enviroments/$env plan -out=tfplan
terraform -chdir=infra/enviroments/$env apply -auto-approve tfplan

# To access your key vault, use the user-assigned managed identity
client_id=$(az aks show -g $resource_group_name -n $cluster_name --query identityProfile.kubeletidentity.clientId -o tsv)

# Set policy to access secrets in your key vault
az keyvault set-policy -n $key_vault_name --secret-permissions get --spn $client_id

# Add secrets to AKV
sh deploy/azure-aks/add_akv_secrets.sh $env

# Configure kubectl to connect to your Kubernetes cluster
az aks get-credentials --resource-group $resource_group_name --name $cluster_name

# Create Azure Key Vault Provider for Secrets Store CSI Driver support
sh deploy/azure-aks/create_akv_csi_driver.sh $key_vault_name

# Set Azure Key Vault CSI secret provider class.
sed 's/$keyvaultName/microservicedemokv'"$env"'/g' kubernetes/base/secretproviderclass.yml | \
sed 's/$tenantId/'"$(az account list | jq -r '.[].tenantId')"'/g' | \
sed 's/$clientId/'"$client_id"' /g'| \
kubectl apply -f -

# Build services
docker build -t microservice_demo/order-api:latest -f src/modules/order/backend/Dockerfile .
docker build -t microservice_demo/identity-api:latest -f src/modules/identity/backend/Dockerfile .
docker build -t microservice_demo/frontend:latest -f src/app/frontend/Dockerfile .
docker build -t microservice_demo/gateway:latest -f src/gateway/Dockerfile .

# Tag based on ACR login path
docker tag microservice_demo/order-api:latest microservicedemoacr$env.azurecr.io/order-api:latest
docker tag microservice_demo/identity-api:latest microservicedemoacr$env.azurecr.io/identity-api:latest
docker tag microservice_demo/frontend:latest microservicedemoacr$env.azurecr.io/frontend:latest

# Login to ACR
az acr login --name microservicedemoacr$env

# Push to ACR
docker push microservicedemoacr$env.azurecr.io/order-api:latest
docker push microservicedemoacr$env.azurecr.io/identity-api:latest
docker push microservicedemoacr$env.azurecr.io/frontend:latest

# Install RabbitMq cluster operator
kubectl rabbitmq install-cluster-operator

# Deploy services
kubectl apply -k kubernetes/enviroments/$env

# TODO: Gateway
```

Destroy 
```bash
# Set enviroment to prod or beta
env=beta

# Destroy Azure infrastructure
terraform -chdir=infra/enviroments/$env destroy -auto-approve

# Delete Terraform state backend resources
az group delete --name microservice_demo_tfstate_rg --yes

# Remove AKV
az keyvault purge --name microservicedemokv$env

# Remove docker images
docker rmi microservicedemoacr$env/identity-api:latest
docker rmi microservicedemoacr$env/order-api:latest
docker rmi microservicedemoacr$env/frontend:latest

# Remove volumes
rm -R .volumes
```
