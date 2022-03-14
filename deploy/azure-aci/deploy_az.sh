location=northeurope
rg_name=microservice-demo-rg
acr_name=microservicedemoacr
env=beta

az group create --name $rg_name --location $location

az acr create --resource-group $rg_name --name $acr_name --sku Basic

az acr login --name $acr_name

docker-compose -f deploy/azure-aci/docker-compose.$env.yml --build -d

docker-compose -f deploy/azure-aci/docker-compose.$env.yml down

docker-compose -f deploy/azure-aci/docker-compose.$env.yml push

az acr repository show --name $acr_name --repository azure-vote-front

docker login azure

docker context create aci myacicontext

docker context ls

docker context use myacicontext

docker-compose -f deploy/azure-aci/docker-compose.$env.yml up

docker-compose -f deploy/azure-aci/docker-compose.$env.yml down