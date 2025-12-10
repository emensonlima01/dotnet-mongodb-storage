using Domain.Repositories;
using Infrastructure.Configuration;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace IoC;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMongoDb(configuration)
            .AddRepositories();
        
        return services;
    }

    private static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(
            configuration.GetSection("MongoDbSettings"));

        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>()
                ?? throw new InvalidOperationException("MongoDbSettings not found in configuration");
            
            return new MongoClient(settings.ConnectionString);
        });
        
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IPaymentRepository, PaymentRepository>();
        
        return services;
    }
}
