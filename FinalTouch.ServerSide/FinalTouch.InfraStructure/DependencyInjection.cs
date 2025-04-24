using FinalTouch.Core.Interfaces;
using FinalTouch.InfraStructure.Data;
using FinalTouch.InfraStructure.Services;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IProductCommandRepository<>), typeof(ProductCommandRepository<>));
        services.AddScoped(typeof(IProductQueryRepository<>), typeof(ProductQueryRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IResponseCacheService, ResponseCacheService>();

        return services;
    }
}
