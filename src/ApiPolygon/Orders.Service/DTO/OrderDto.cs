using AutoMapper;
using Core.Data.DTO;
using Orders.Data.Models;

namespace Orders.Service.DTO;

public record OrderDto(
    Guid Id,
    string Name,
    decimal Price
) : BaseDto(Id), IMapFrom
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderDto>().ReverseMap();
    }
}