deploy()
{
	# Create self signed sertificate (not tested)
	openssl req -x509 -newkey rsa:4096 -sha256 -nodes -keyout deploy/docker-compose/certs/tls.key -out deploy/docker-compose/certs/tls.crt -subj "/CN=microservice-demo.com" -days 365

    # Build services
    docker build -t microservice_demo/order-api:latest -f src/modules/Order/backend/Dockerfile .
    docker build -t microservice_demo/identity-api:latest -f src/modules/Identity/backend/Dockerfile .
    docker build -t microservice_demo/frontend:latest -f src/core/frontend/app/Dockerfile .

	# Deploy docker compose
	docker-compose -f deploy/local/docker-compose.yml up
}

destroy()
{
	docker-compose -f deploy/local/docker-compose.yml down

	# Remove docker images
	docker rmi microservice_demo/identity-api:latest
	docker rmi microservice_demo/order-api:latest
	docker rmi microservice_demo/frontend:latest

	# Remove self signed sertificate
	rm deploy/local/certs/*

	# Remove SQL Server volume
	rm deploy/local/data/*
}

"$@"