# Source: https://azure.github.io/secrets-store-csi-driver-provider-azure/demos/standard-walkthrough/

echo "Create Azure Key Vault Provider for Secrets Store CSI Driver support"

key_vault_name=$1

# Deploy the Azure Key Vault Provider and Secrets Store CSI Driver components
helm repo add csi-secrets-store-provider-azure https://raw.githubusercontent.com/Azure/secrets-store-csi-driver-provider-azure/master/charts
helm install csi csi-secrets-store-provider-azure/csi-secrets-store-provider-azure

# Create a service principal to access keyvault
service_principal_client_secret="$(az ad sp create-for-rbac --skip-assignment -n $key_vault_name --query 'password' -otsv)"
service_principal_client_id="$(az ad sp list --display-name $key_vault_name | jq -r '.[].appId')"

# Set the access policy for keyvault objects
az keyvault set-policy -n $key_vault_name --secret-permissions get --spn $service_principal_client_id

# Create the Kubernetes secret with the service principal credentials
kubectl delete secret secrets-store-creds
kubectl create secret generic secrets-store-creds --from-literal clientid=$service_principal_client_id --from-literal clientsecret=$service_principal_client_secret
kubectl label secret secrets-store-creds secrets-store.csi.k8s.io/used=true 