namespace UrlCompressor.Infrastructure.Configuration;

public struct DatabaseConfiguration
{
    public enum DatabaseProvider
    {
        Sqlite,
        MySql
    }
    
    public DatabaseProvider Provider { get; set; }
    public string ConnectionString { get; set; }

    public DatabaseConfiguration(DatabaseProvider provider, string connectionString)
    {
        Provider = provider;
        ConnectionString = connectionString;
    }
}