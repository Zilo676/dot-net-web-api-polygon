using AutoMapper;
using Core.Utils.BaseService;
using Core.Utils.Repository;
using Core.Utils.Unit;
using Orders.Data.Models;
using Orders.Service.DTO;
using Orders.Service.Interfaces;

namespace Orders.Service.Services;

public class OrdersService(IRepository<Order> repository, IMapper mapper, IUnitOfWork unit)
    : BaseService<Order, OrderDto>(repository, mapper, unit), IOrdersService;