# Microservice architecture demo

Full stack test automation demo

* Unit testing with xUnit
* Integration testing with xUnit, SQL and in memory database 
* UI testing with Flutter
* Infrastructure testing with Terraform in Azure
* Kubernetes deployment testing
* Load and stress testing
* Security tests

## Tips and Tricks

Create docker image
```bash
docker build -t test_automation_demo/order-api:latest -f src/modules/order/backend/Dockerfile .
```
Update migrations
```bash
dotnet ef migrations add "Initial" --project src/modules/order/backend/Order.Infrastructure --startup-project src/modules/order/backend/Order.Api
```