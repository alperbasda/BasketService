using _1likte.Application.Contracts;
using _1likte.Persistence.Repositories;
using Core.Persistence.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1likte.Persistence;

public static class PersistenceServiceRegistrations
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseOptions = services.AddSettingsSingleton<DatabaseOptions>(configuration);
        
        services.AddScoped<IBasketDal, BasketDal>();
        
        return services;
    }


    public static T AddSettingsSingleton<T>(this IServiceCollection services, IConfiguration configuration)
    where T : class, new()
    {
        T settings = new T();
        configuration.GetSection(settings.GetType().Name).Bind(settings);

        services.AddSingleton(sp =>
        {
            return settings;
        });
        return settings;
    }
}
