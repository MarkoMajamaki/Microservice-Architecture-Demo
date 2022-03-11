aks()
{
    env=$1
    echo "Destroy $env enviroment"

    # Destroy Azure infrastructure
    terraform -chdir=infra/enviroments/$env destroy -auto-approve

    # Delete Terraform state backend resources
    az group delete --name microservice_demo_tfstate_rg
}

aci()
{

}

as()
{
    
}

"$@"