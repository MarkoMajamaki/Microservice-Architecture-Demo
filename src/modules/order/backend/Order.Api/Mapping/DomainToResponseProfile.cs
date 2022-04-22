using AutoMapper;
using Order.Application;

namespace Order.Api;

public class DomainToResponseProfile : Profile
{
    public DomainToResponseProfile()
    {
        CreateMap<Order.Domain.OrderItem, GetOrderResponse.OrderItem>();
        
        CreateMap<Order.Domain.Order, GetOrderResponse>()
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status.Code))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id));

        CreateMap<GetAllOrders.Response, List<GetOrderResponse>>();
    }
}