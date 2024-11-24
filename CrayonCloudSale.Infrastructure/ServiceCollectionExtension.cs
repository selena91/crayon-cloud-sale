using CrayonCloudSale.Core;
using CrayonCloudSale.Infrastructure.Data;
using CrayonCloudSale.Infrastructure.Data.Models;
using CrayonCloudSale.Infrastructure.GenericRepository;
using CrayonCloudSale.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrayonCloudSale.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfigurationSection azureConfiguration)
    {
        var connStr = azureConfiguration[nameof(AzureConfiguration.ConnectionString)];

        services.AddDbContext<CloudSaleContext>(options =>
        {
            options.UseSqlServer(connStr, b => b.MigrationsAssembly("CrayonCloudSale.Api"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped<IRepository<Account>, Repository<Account, CloudSaleContext>>();
        services.AddScoped<IRepository<Customer>, Repository<Customer, CloudSaleContext>>();
        services.AddScoped<IRepository<PurchasedSoftware>, Repository<PurchasedSoftware, CloudSaleContext>>();

        return services;
    }
}

