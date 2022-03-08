# Start sql server and RabbitMq
docker-compose -p microservice_demo -f "deploy/docker-compose/docker-compose.yml" up -d --build rabbitmq sqlserver

# Wait containers
sleep 5

# Populate databases
sh scripts/populate_databases.sh