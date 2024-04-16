namespace Cinema.Infrastructure.Settings
{
    public class MongoDbSettings(string connectionString, string databaseName)
    {
        public string ConnectionString { get; set; } = connectionString;
        public string DatabaseName { get; set; } = databaseName;
    }
}
