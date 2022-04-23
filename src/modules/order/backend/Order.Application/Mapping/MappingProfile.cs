using AutoMapper;

namespace Order.Application;

/// <summary>
/// Mapping objects between Application and Domain layer
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<IEnumerable<Domain.Order>, Order.Application.GetAllOrders.Response>();

        CreateMap<Domain.Order, Order.Application.GetOrderById.Response>();
        CreateMap<Domain.OrderItem, Order.Application.GetOrderById.Response.OrderItem>();
        CreateMap<Domain.Address, Order.Application.GetOrderById.Response.Address>();
     }
}