using Core.Utils.Repository;
using Microsoft.Extensions.DependencyInjection;
using Orders.Data.Models;

namespace Orders.Data;

public static class OrdersRepositoriesServiceCollectionExtensions
{
    public static IServiceCollection AddOrdersRepositories(this IServiceCollection collection)
    {
        collection.AddScoped<IRepository<Order>, OrdersEfRepository>();
        return collection;
    }
}