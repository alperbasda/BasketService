using _1likte.Application.Base;
using _1likte.Application.ExternalServiceConnectors.Consumers.Merchants;
using _1likte.Application.ExternalServiceConnectors.Consumers.Products;
using _1likte.Application.ExternalServiceConnectors.HttpClients.Merchants;
using _1likte.Application.ExternalServiceConnectors.HttpClients.Products;
using _1likte.Events.Queues;
using Core.CrossCuttingConcerns.Helpers.EnumHelpers;
using MassTransit;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace _1likte.Application;

public static class ApplicationServiceRegistrations
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var execAssembly = Assembly.GetExecutingAssembly();

        services.AddSubClassesOfType(execAssembly, typeof(BaseBusinessRules));
        services.AddAutoMapper(execAssembly);

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(execAssembly);

            // Performans iyileştirmesi gerekli olduğu için ve TDD ye dahil etmek istemediğim için commentte bırakıyorum.
            //configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            //configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
            //configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            //configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));

            ///Dikkat.Aynı anda dbye erişmesi gereken işlemlerde publish kullanırsan ef core concurrency fırlatır.
            configuration.NotificationPublisher = new TaskWhenAllPublisher();
            configuration.NotificationPublisherType = typeof(TaskWhenAllPublisher);
            configuration.Lifetime = ServiceLifetime.Scoped;
        });

        services.AddHttpClient<IMerchantClient, MerchantClient>((client) =>
        {
            client.BaseAddress = new Uri(configuration["ApiEndpointSettings:MerchantService"]);
        });

        services.AddHttpClient<IProductClient, ProductClient>((client) =>
        {
            client.BaseAddress = new Uri(configuration["ApiEndpointSettings:ProductService"]);
        });

        return services;
    }

    public static void AddAmqpServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<MerchantInfoChangedEventConsumer>();
        services.AddScoped<ProductInfoChangedEventConsumer>();
        
        services.AddMassTransit(x =>
        {
            x.AddConsumer<MerchantInfoChangedEventConsumer>();
            x.AddConsumer<ProductInfoChangedEventConsumer>();
            
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:Host"], ushort.Parse(configuration["RabbitMq:Port"]), "/", host =>
                {
                    host.Username(configuration["RabbitMq:UserName"]);
                    host.Password(configuration["RabbitMq:Password"]);
                });

                cfg.ReceiveEndpoint(QueueNames.MerchantInfoChangedQueue.GetDescription(), e =>
                {
                    e.PrefetchCount = 100;
                    e.ConfigureConsumer(ctx, typeof(MerchantInfoChangedEventConsumer));

                });

                cfg.ReceiveEndpoint(QueueNames.ProductInfoChangedQueue.GetDescription(), e =>
                {
                    e.PrefetchCount = 100;
                    e.ConfigureConsumer(ctx, typeof(ProductInfoChangedEventConsumer));

                });

            });
        });

        services.Configure<MassTransitHostOptions>(options =>
        {
            options.WaitUntilStarted = false;
            options.StartTimeout = TimeSpan.FromSeconds(5);
            options.StopTimeout = TimeSpan.FromMinutes(1);
        });

    }

    public static IServiceCollection AddSubClassesOfType(
       this IServiceCollection services,
       Assembly assembly,
       Type type,
       Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }

}
