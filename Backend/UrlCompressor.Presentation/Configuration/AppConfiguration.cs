using UrlCompressor.Application.Common.Configuration;
using UrlCompressor.Infrastructure.Configuration;

namespace UrlCompressor.Presentation.Configuration;

public class AppConfiguration(IConfiguration configuration) : IApplicationConfiguration, IInfrastructureConfiguration
{
    public ApplicationConfiguration ApplicationConfiguration { get; } = new ApplicationConfiguration
    {
        RequireUniqueUrls = configuration["ApplicationConfiguration:RequireUniqueUrls"]?.ToLower() == "true",
        AllowSearchByInitialUrl = configuration["ApplicationConfiguration:RequireUniqueUrls"]?.ToLower() == "true",
        ChangeSmallUrlOnUpdate = configuration["ApplicationConfiguration:RequireUniqueUrls"]?.ToLower() == "true",
        Host = configuration[WebHostDefaults.ServerUrlsKey] ?? throw new InvalidOperationException()
    };

    public DatabaseConfiguration DatabaseConfiguration { get; } = new DatabaseConfiguration
    {
        Provider = Enum.Parse<DatabaseConfiguration.DatabaseProvider>(configuration["DatabaseConfiguration:Provider"] ?? "Sqlite"),
        ConnectionString = configuration["DatabaseConfiguration:ConnectionString"] ?? throw new InvalidOperationException()
    };
}