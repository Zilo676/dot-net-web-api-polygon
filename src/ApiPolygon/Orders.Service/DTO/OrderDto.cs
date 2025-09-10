using AutoMapper;
using Core.Data.DTO;
using Orders.Data.Models;

namespace Orders.Service.DTO;

public record OrderDto : BaseDto, IMapFrom
{
    public string Name { get; init; }
    decimal Price { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderDto>().ReverseMap();
    }
}