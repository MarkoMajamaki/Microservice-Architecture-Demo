ENV=$1
KEY_VAULT_NAME=microservicedemokv$ENV

az keyvault secret set --vault-name $KEY_VAULT_NAME --name "IDENTITY--CONNECTIONSTRING" --value "TODO"
az keyvault secret set --vault-name $KEY_VAULT_NAME --name "ORDER--CONNECTIONSTRING" --value "TODO"

az keyvault secret set --vault-name $KEY_VAULT_NAME --name "RABBITMQ--HOSTNAME" --value "rabbitmq.architecture-demo.svc.cluster.local"
az keyvault secret set --vault-name $KEY_VAULT_NAME --name "RABBITMQ--PORT" --value "5672"
az keyvault secret set --vault-name $KEY_VAULT_NAME --name "RABBITMQ--USERNAME" --value "guest"
az keyvault secret set --vault-name $KEY_VAULT_NAME --name "RABBITMQ--PASSWORD" --value "guest"

az keyvault secret set --vault-name $KEY_VAULT_NAME --name "FACEBOOK--APPID" --value "Your_Facebook_AppId"
az keyvault secret set --vault-name $KEY_VAULT_NAME --name "FACEBOOK--APPSECRET" --value "Your_Facebook_AppSecret"
az keyvault secret set --vault-name $KEY_VAULT_NAME --name "GOOGLE--CLIENTID" --value "Your_Google_ClientId"
az keyvault secret set --vault-name $KEY_VAULT_NAME --name "GOOGLE--CLIENTSECRET" --value "Your_Google_Client_Secret"

az keyvault secret set --vault-name $KEY_VAULT_NAME --name "JWT--SECRET" --value "ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM" # Only for testing!
az keyvault secret set --vault-name $KEY_VAULT_NAME --name "JWT--VALIDAUDIENCE" --value "Your_JWT_Validaudience"
az keyvault secret set --vault-name $KEY_VAULT_NAME --name "JWT--VALIDISSUER" --value "Your_JWT_ValidIssuer"