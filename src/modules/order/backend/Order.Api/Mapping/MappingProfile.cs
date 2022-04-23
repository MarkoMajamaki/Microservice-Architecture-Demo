using AutoMapper;

namespace Order.Api;

/// <summary>
/// Mapping objects between Api and Application layer
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order.Api.CreateOrderRequest, Order.Application.CreateOrder.Command>();
        CreateMap<Order.Api.CreateOrderRequest.OrderItem, Order.Application.CreateOrder.Command.OrderItem>();
        CreateMap<Order.Api.CreateOrderRequest.Address, Order.Application.CreateOrder.Command.Address>();

        CreateMap<Order.Application.CreateOrder.Response, Order.Api.CreateOrderResponse>()
            .ForMember(dest => dest.StatusCode, opt => opt.MapFrom(src => src.status.Code));            

        CreateMap<Order.Application.GetOrderById.Response, Order.Api.GetOrderResponse>();
        CreateMap<Order.Application.GetOrderById.Response.OrderItem, Order.Api.GetOrderResponse.OrderItem>();
        CreateMap<Order.Application.GetOrderById.Response.Address, Order.Api.GetOrderResponse.Address>();
    }
}