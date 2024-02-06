using Microsoft.EntityFrameworkCore.Storage;
using UrlCompressor.Application.Common.Configuration;
using UrlCompressor.Infrastructure.Configuration;

namespace UrlCompressor.Presentation.Configuration;

public class AppConfiguration() : IApplicationConfiguration, IInfrastructureConfiguration
{
    public ApplicationConfiguration ApplicationConfiguration { get; } = new ApplicationConfiguration
    {
        RequireUniqueUrls = true,
        AllowSearchByInitialUrl = true,
        ChangeSmallUrlOnUpdate = true,
    };

    public DatabaseConfiguration DatabaseConfiguration { get; } = new DatabaseConfiguration
    {
        Provider = DatabaseProvider.Sqlite,
        ConnectionString = "Data Source=UrlCompressor.db"
    };
}