using Order.Domain;

namespace Order.Application;

public record CreateOrderResponse(int Id, Status status);