using AutoMapper;
using DutchTreatHW.Data.Entities;
using DutchTreatHW.ViewModels;

namespace DutchTreatHW.Data;

public class DutchMappingProfile : Profile
{
    public DutchMappingProfile()
    {
        CreateMap<Order, OrderViewModel>()
            .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
            .ReverseMap();

        CreateMap<OrderItem, OrderItemViewModel>()
            .ReverseMap();
    }
}
