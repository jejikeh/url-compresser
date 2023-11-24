using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UrlCompressor.Application.Services;
using UrlCompressor.Infrastructure.Persistence;
using UrlCompressor.Infrastructure.Services;

namespace UrlCompressor.Infrastructure.Configuration;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IInfrastructureConfiguration configuration)
    {
        return services
            .AddDbContext(configuration)
            .AddRepositories()
            .AddServices();
    }
    
    private static IServiceCollection AddDbContext(this IServiceCollection services, IInfrastructureConfiguration configuration)
    {
        return configuration.DatabaseConfiguration.Provider switch
        {
            DatabaseConfiguration.DatabaseProvider.Sqlite => services.AddSqliteDbContext(configuration.DatabaseConfiguration.ConnectionString),
            DatabaseConfiguration.DatabaseProvider.MySql => services.AddMySqlDbContext(configuration.DatabaseConfiguration.ConnectionString),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static IServiceCollection AddSqliteDbContext(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<UrlCompressorDbContext>(options =>
        {
            options.UseSqlite(
                connectionString,
                b => b.MigrationsAssembly(typeof(UrlCompressorDbContext).Assembly.FullName));
        });
    }
    
    private static IServiceCollection AddMySqlDbContext(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<UrlCompressorDbContext>(options =>
        {
            options.UseMySQL(
                connectionString,
                b => b.MigrationsAssembly(typeof(UrlCompressorDbContext).Assembly.FullName));
        });
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddScoped<ICompressedUrlRepository, CompressedUrlRepository>();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<IUrlCompressorService, RandomStringService>();
    }
}