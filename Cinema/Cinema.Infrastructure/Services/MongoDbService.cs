using MongoDB.Driver;

namespace Cinema.Infrastructure.Services
{
    public class MongoDbService(IMongoDatabase database)
    {
        private readonly IMongoDatabase _database = database;

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
