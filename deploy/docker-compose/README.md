# Deploy locally using docker compose

## Deploy
```bash
docker-compose -f deploy/docker-compose/docker-compose.yml up
```

## Destroy 
```bash
docker-compose -f deploy/docker-compose/docker-compose.yml down

# Remove local docker images
docker rmi microservice_demo/identity-api:latest
docker rmi microservice_demo/order-api:latest
docker rmi microservice_demo/frontend:latest
docker rmi envoyproxy/envoy:v1.20.2
docker rmi mcr.microsoft.com/mssql/server:latest
docker rmi rabbitmq:3-management

# Remove volumes
rm -R .volumes
```
