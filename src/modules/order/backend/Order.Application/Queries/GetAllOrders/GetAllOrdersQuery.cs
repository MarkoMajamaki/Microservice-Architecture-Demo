using MediatR;

namespace Order.Application;
public record GetAllOrdersQuery() : IRequest<GetAllOrdersResponse>;