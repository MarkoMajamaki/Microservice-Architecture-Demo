# Create cluster with config
kind create cluster --name microservice-demo --config deploy/kind/kind.config

# Build services
docker build -t microservice_demo/order-api:latest -f src/modules/Order/backend/Dockerfile .
docker build -t microservice_demo/identity-api:latest -f src/modules/Identity/backend/Dockerfile .
docker build -t microservice_demo/frontend:latest -f src/core/frontend/app/Dockerfile .

# Load images to cluster
kind load docker-image microservice_demo/order-api:latest --name microservice-demo
kind load docker-image microservice_demo/identity-api:latest --name microservice-demo
kind load docker-image microservice_demo/frontend:latest --name microservice-demo

# Check loaded images: docker exec -it microservice-demo-control-plane crictl images

# Install RabbitMq cluster operator (install plugin first!) 
kubectl rabbitmq install-cluster-operator

# Deploy ingress controller (REPLACE WITH ENVOY)
# kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/static/provider/kind/deploy.yaml

# Deploy development enviroment services
kubectl apply -k kubernetes/enviroments/dev