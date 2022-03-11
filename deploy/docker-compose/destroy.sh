docker-compose -f deploy/local/docker-compose.yml down

# Remove docker images
docker rmi microservice_demo/identity-api:latest
docker rmi microservice_demo/order-api:latest
docker rmi microservice_demo/frontend:latest

# Remove volumes
rm -R .volumes