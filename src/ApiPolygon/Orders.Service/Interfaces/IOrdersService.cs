using Core.Utils.BaseService;
using Orders.Data.Models;
using Orders.Service.DTO;

namespace Orders.Service.Interfaces;

public interface IOrdersService : IBaseService<Order, OrderDto>
{
}