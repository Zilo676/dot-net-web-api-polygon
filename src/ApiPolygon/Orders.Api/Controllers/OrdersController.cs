using Core.Utils.BaseController;
using Core.Utils.BaseService;
using Microsoft.AspNetCore.Mvc;
using Orders.Data.Models;
using Orders.Service.DTO;
using Orders.Service.Interfaces;

namespace Orders.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : CrudController<Order, OrderDto>
{
    public OrdersController(IOrdersService service, ILogger<CrudController<Order, OrderDto>> logger) :
        base(service, logger)
    {
        
    }
}