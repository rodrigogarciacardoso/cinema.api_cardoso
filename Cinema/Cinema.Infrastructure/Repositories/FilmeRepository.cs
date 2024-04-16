using Cinema.Domain.Entidades;
using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Data.MongoDb.Documents;
using MongoDB.Driver;

namespace Cinema.Infrastructure.Repositories
{
    public class FilmeRepository(IMongoDatabase database) : IFilmeRepository
    {
        private readonly IMongoCollection<FilmeDocument> _collection = database.GetCollection<FilmeDocument>("Filmes");

        public async Task<Filme> GetFilmeAsync(string id)
        {
            var filmeDocument = await _collection.Find(f => f.Id == id).FirstOrDefaultAsync();
            return MapToDomain(filmeDocument);
        }

        public async Task<IEnumerable<Filme>> GetFilmesAsync()
        {
            var filmeDocuments = await _collection.Find(f => true).ToListAsync();
            return filmeDocuments.Select(MapToDomain);
        }

        private Filme MapToDomain(FilmeDocument filmeDocument)
        {
            return new Filme
            {
                Id = filmeDocument.Id,
                Titulo = filmeDocument.Titulo,
                Diretor = filmeDocument.Diretor
            };
        }
    }

}
