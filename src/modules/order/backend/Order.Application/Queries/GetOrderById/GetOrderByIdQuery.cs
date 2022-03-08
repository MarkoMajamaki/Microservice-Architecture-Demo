using MediatR;

namespace Order.Application;

public record GetOrderByIdQuery(int id) : IRequest<GetOrderByIdResponse>;