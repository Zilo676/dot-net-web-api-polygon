using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.DependencyInjection;
using Orders.Service.Interfaces;
using Orders.Service.Services;

namespace Orders.Service;

public static class OrdersServiceCollectionExtension
{
    public static IServiceCollection AddOrdersCrudServices(
        this IServiceCollection collection
    )
    {
        collection.AddScoped<IOrdersService, OrdersService>();
        
        collection.AddAutoMapper(cfg => { cfg.AddExpressionMapping(); }, typeof(MappingProfile).Assembly);
        return collection;
    }
}