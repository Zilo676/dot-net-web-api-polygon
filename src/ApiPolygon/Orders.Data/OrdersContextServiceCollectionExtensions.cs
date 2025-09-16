using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Orders.Data;

public static class OrdersContextServiceCollectionExtensions
{
    public static IServiceCollection AddOrdersDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<OrdersContext>(options => { options.UseSqlServer(connectionString); });
        return services;
    }
}