using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.EFOptimisation;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _context;
    

    public OrderController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _context.Orders
            .Include(x => x.Customer).ThenInclude(x => x.Name)
            .Include(x => x.Items).ThenInclude(x => x.Count).ToListAsync();
        // Загружает только Orders

        var result = orders.Select(o => new OrderDto
        {
            Id = o.Id,
            CreatedAt = o.CreatedAt,
            CustomerName = o.Customer.Name, 
            ItemsCount = o.Items.Count
        }).ToList();

        return Ok(result);
    }
}