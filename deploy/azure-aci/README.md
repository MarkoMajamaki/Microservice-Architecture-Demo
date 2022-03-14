# Deploy to Azure Container Instances

## Create infra with Terraform
```bash
# Set enviroment to prod or beta
env=beta

# Create TF state storage
sh infra/create_tfstate_storage.sh

# Init infrastructure with Terraform, do plan, and apply plan to create base infrastructure
terraform -chdir=infra/enviroments/$env init
terraform -chdir=infra/enviroments/$env plan -out=tfplan
terraform -chdir=infra/enviroments/$env apply -auto-approve tfplan
```

## Destroy infra with Terraform
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

## Create infra with Azure CLI
```bash
env=beta # beta/prod

location=northeurope
rg_name=microservice-demo-$env-rg
acr_name=microservicedemoacr$env

# Login to Azure
az login

# Create resource group
az group create --name $rg_name --location $location

# Create ACR
az acr create --resource-group $rg_name --name $acr_name --sku Basic --admin-enabled true
```

## Destroy infra with Azure CLI
```bash
# Destroy resource group
az group delete --name $rg_name --yes
```

## Run containers in ACI
```bash
# Login to ACR
az acr login --name microservicedemoacr$env

# Build images
docker-compose -f deploy/azure-aci/docker-compose.$env.yml build

# Push images to ACR
docker-compose -f deploy/azure-aci/docker-compose.$env.yml push

# Login to azure with docker
docker login azure

# Create an ACI context. This context associates Docker with an Azure subscription and resource group so you can create and manage container instances. (check: docker context ls)
docker context create aci acicontext --resource-group $rg_name

# Make docker command to run in ACI
docker context use acicontext

# Run containers in ACI (TODO: Ports should be unique for each container)
docker-compose -f deploy/azure-aci/docker-compose.$env.yml --project-name aci up

# Remove containers from ACI
docker-compose -f deploy/azure-aci/docker-compose.$env.yml --project-name aci down 
```