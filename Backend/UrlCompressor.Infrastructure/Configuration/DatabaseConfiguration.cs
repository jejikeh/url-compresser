namespace UrlCompressor.Infrastructure.Configuration;

public enum DatabaseProvider
{
    Sqlite,
    MySql
}

public struct DatabaseConfiguration
{
    
    public DatabaseProvider Provider { get; set; }
    public string ConnectionString { get; set; }

    public DatabaseConfiguration(DatabaseProvider provider, string connectionString)
    {
        Provider = provider;
        ConnectionString = connectionString;
    }
}