# Source: https://azure.github.io/secrets-store-csi-driver-provider-azure/demos/standard-walkthrough/

echo "Create Azure Key Vault Provider for Secrets Store CSI Driver support"

CLUSTER_NAME=$1
RESOURCE_GROUP_NAME=$2
KEY_VAULT_NAME=$3
TENANT_ID=$(az account list | jq -r '.[].tenantId')

# Deploy the Azure Key Vault Provider and Secrets Store CSI Driver components
helm repo add csi-secrets-store-provider-azure https://raw.githubusercontent.com/Azure/secrets-store-csi-driver-provider-azure/master/charts
helm install csi csi-secrets-store-provider-azure/csi-secrets-store-provider-azure

# Create a service principal to access keyvault
SERVICE_PRINCIPAL_CLIENT_SECRET="$(az ad sp create-for-rbac --skip-assignment -n $KEY_VAULT_NAME --query 'password' -otsv)"
SERVICE_PRINCIPAL_CLIENT_ID="$(az ad sp list --display-name $KEY_VAULT_NAME | jq -r '.[].appId')"

# Set the access policy for keyvault objects
az keyvault set-policy -n $KEY_VAULT_NAME --secret-permissions get --spn $SERVICE_PRINCIPAL_CLIENT_ID

# Create the Kubernetes secret with the service principal credentials
kubectl delete secret secrets-store-creds
kubectl create secret generic secrets-store-creds --from-literal clientid=$SERVICE_PRINCIPAL_CLIENT_ID --from-literal clientsecret=$SERVICE_PRINCIPAL_CLIENT_SECRET
kubectl label secret secrets-store-creds secrets-store.csi.k8s.io/used=true 