using Cinema.Infrastructure.Settings;
using MongoDB.Driver;

namespace Cinema.Infrastructure.Services
{
    public class MongoDbService(IMongoClient client, MongoDbSettings settings)
    {
        private readonly IMongoDatabase _database = client.GetDatabase(settings.DatabaseName);

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }

}
