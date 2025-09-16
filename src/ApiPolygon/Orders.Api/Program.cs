using Core.Utils.Unit;
using Microsoft.EntityFrameworkCore;
using Orders.Data;
using Orders.Service;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace Orders.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler =
                System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddEnumsWithValuesFixFilters();
            options.SupportNonNullableReferenceTypes();
        });

        // Inject db context
        builder.Services.AddOrdersDbContext(builder.Configuration.GetConnectionString("OrdersDb"));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Inject repositories
        builder.Services.AddOrdersRepositories();

        // Inject crud services
        builder.Services.AddOrdersCrudServices();

        var app = builder.Build();

// Configure the HTTP request pipeline.


        // if (!app.Environment.IsProduction())
        // {
        //     app.UseSwagger();
        //     app.UseSwaggerUI();
        // }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HttpBin Proxy API V1");
                c.RoutePrefix = "swagger"; // Доступ по /swagger
            });
      

        // if (app.Environment.EnvironmentName != "ProductionStage")
        // {
        //     using (var scope = app.Services.CreateScope())
        //     {
        //         var services = scope.ServiceProvider;

        //         var context = services.GetRequiredService<OrdersContext>();
        //         context.Database.Migrate();
        //     }
        // }

        // if (app.Environment.EnvironmentName != "ProductionStage")
        // {
        //     app.UseHttpsRedirection();
        // }
        app.UseHttpsRedirection();
        // Add custom global exception handler middleware
        // Add Serilog
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}