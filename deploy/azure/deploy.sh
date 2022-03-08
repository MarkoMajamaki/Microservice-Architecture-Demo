# Deployment to Azure
deploy()
{    
    ENV=$1
    CLUSTER_NAME=microservicedemoaks$ENV
    RESOURCE_GROUP_NAME=microservice-demo-$ENV-rg
    KEY_VAULT_NAME=microservicedemokv$ENV

    echo "Deploy $ENV enviroment"

    # Create Terraform state backend
    sh infra/create_tfstate_backend.sh

    # Init infrastructure with Terraform, do plan, and apply plan to create base infrastructure
    terraform -chdir=infra/enviroments/$ENV init
    terraform -chdir=infra/enviroments/$ENV plan -out=tfplan
    terraform -chdir=infra/enviroments/$ENV apply -auto-approve tfplan

    # To access your key vault, use the user-assigned managed identity
    CLIENT_ID=$(az aks show -g $RESOURCE_GROUP_NAME -n $CLUSTER_NAME --query identityProfile.kubeletidentity.clientId -o tsv)

    # Set policy to access secrets in your key vault
    az keyvault set-policy -n $KEY_VAULT_NAME --secret-permissions get --spn $CLIENT_ID

    # Add secrets to AKV
    sh deploy/azure/add_akv_secrets.sh $ENV

    # Configure kubectl to connect to your Kubernetes cluster
    az aks get-credentials --resource-group $RESOURCE_GROUP_NAME --name $CLUSTER_NAME

    # Create Azure Key Vault Provider for Secrets Store CSI Driver support
    sh deploy/azure/create_akv_csi_driver.sh $CLUSTER_NAME $RESOURCE_GROUP_NAME $KEY_VAULT_NAME

    # Set Azure Key Vault CSI secret provider class.
    sed 's/$keyvaultName/microservicedemokv'"$ENV"'/g' kubernetes/base/secretproviderclass.yml | \
    sed 's/$tenantId/'"$(az account list | jq -r '.[].tenantId')"'/g' | \
    sed 's/$clientId/'"$CLIENT_ID"' /g'| \
    kubectl apply -f -

    # Build services
    docker build -t microservice_demo/order-api:latest -f src/modules/order/backend/Dockerfile .
    docker build -t microservice_demo/identity-api:latest -f src/modules/identity/backend/Dockerfile .
    docker build -t microservice_demo/frontend:latest -f src/core/frontend/app/Dockerfile .

    # Tag based on ACR login path
    docker tag microservice_demo/order-api:latest microservicedemoacr$ENV.azurecr.io/order-api:latest
    docker tag microservice_demo/identity-api:latest microservicedemoacr$ENV.azurecr.io/identity-api:latest
    docker tag microservice_demo/frontend:latest microservicedemoacr$ENV.azurecr.io/frontend:latest

    # Login to ACR
    az acr login --name microservicedemoacr$ENV

    # Push to ACR
    docker push microservicedemoacr$ENV.azurecr.io/order-api:latest
    docker push microservicedemoacr$ENV.azurecr.io/identity-api:latest
    docker push microservicedemoacr$ENV.azurecr.io/frontend:latest

    # Install RabbitMq cluster operator
    kubectl rabbitmq install-cluster-operator

    # Add the ingress-nginx repository
    helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx

    # Use Helm to deploy an NGINX ingress controller
    helm install nginx-ingress ingress-nginx/ingress-nginx --set controller.replicaCount=1

    # Deploy services
    kubectl apply -k kubernetes/enviroments/$ENV
}

destroy()
{
    ENV=$1
    echo "Destroy $ENV enviroment"

    # Destroy Azure infrastructure
    terraform -chdir=infra/enviroments/$ENV destroy -auto-approve

    # Delete Terraform state backend resources
    az group delete --name microservice_demo_tfstate_rg
}

update_order()
{
    ENV=$1
    docker rmi microservice_demo/identity-api:latest
    docker build -t microservice_demo/order-api:latest -f src/modules/order/backend/Dockerfile .
    docker tag microservice_demo/order-api:latest microservicedemoacr$ENV.azurecr.io/order-api:latest
    docker push microservicedemoacr$ENV.azurecr.io/order-api:latest
    kubectl delete deployment.apps/order-api
    kubectl apply -k kubernetes/enviroments/$ENV
}

update_identity()
{
    ENV=$1
    docker rmi microservice_demo/identity-api:latest
    docker build -t microservice_demo/identity-api:latest -f src/modules/Identity/backend/Dockerfile .
    docker tag microservice_demo/identity-api:latest microservicedemoacr$ENV.azurecr.io/identity-api:latest
    docker push microservicedemoacr$ENV.azurecr.io/identity-api:latest
    kubectl delete deployment.apps/identity-api
    kubectl apply -k kubernetes/enviroments/$ENV
}

"$@"