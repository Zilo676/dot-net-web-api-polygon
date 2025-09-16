using Orders.Data;
using Orders.Data.Models;
using Orders.Data.Repository;

public class OrdersEfRepository(OrdersContext context) : Repository<Order>(context);