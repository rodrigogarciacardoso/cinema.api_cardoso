using Cinema.Domain.Entidades;
using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Services;
using MongoDB.Driver;

namespace Cinema.Infrastructure.Repositories
{
    public class FilmeRepository(MongoDbService mongoDbService) : IFilmeRepository
    {
        private readonly IMongoCollection<Filme> _filmes = mongoDbService.GetCollection<Filme>("Filmes");

        public async Task<Filme> GetFilmeAsync(string id)
        {
            var filter = Builders<Filme>.Filter.Eq(f => f.Id, id);
            return await _filmes.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Filme>> GetFilmesAsync()
        {
            return await _filmes.Find(filme => true).ToListAsync();
        }
    }
}
