namespace WebApplication1.EFOptimisation;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<OrderItem> Items { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public record OrderDto(int Id, DateTime CreatedAt, string CustomerName, int ItemsCount);