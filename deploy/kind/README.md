# Deploy locally using Kind kubernetes cluster

## Deploy
```bash
# Create cluster with config
kind create cluster --name microservice-demo --config deploy/kind/kind.config

# Build services
docker build -t microservice_demo/order-api:latest -f src/modules/Order/backend/Dockerfile .
docker build -t microservice_demo/identity-api:latest -f src/modules/identity/backend/Dockerfile .
docker build -t microservice_demo/frontend:latest -f src/app/frontend/Dockerfile .

# Load images to cluster
kind load docker-image microservice_demo/order-api:latest --name microservice-demo
kind load docker-image microservice_demo/identity-api:latest --name microservice-demo
kind load docker-image microservice_demo/frontend:latest --name microservice-demo

# Check loaded images: docker exec -it microservice-demo-control-plane crictl images

# Install RabbitMq cluster operator (install plugin first!) 
kubectl rabbitmq install-cluster-operator

# Deploy development enviroment services
kubectl apply -k kubernetes/enviroments/dev
```

## Destroy 
```bash
kind delete cluster --name microservice-demo

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
