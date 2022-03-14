# Deploy to Azure Container Instances

## Deploy
```bash
# Set enviroment to prod or beta
env=beta

echo "Deploy $env enviroment to Azure App Services"

sh infra/create_tfstate_storage.sh

# Init infrastructure with Terraform, do plan, and apply plan to create base infrastructure
terraform -chdir=infra/enviroments/$env init
terraform -chdir=infra/enviroments/$env plan -out=tfplan
terraform -chdir=infra/enviroments/$env apply -auto-approve tfplan

# Login to ACR
az acr login --name microservicedemoacr$env

# Push containers to ACR. App Service is updated when images are updated.
docker-compose -f deploy/azure-appservice/docker-compose.$env.yml push
```

## Destroy 
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
