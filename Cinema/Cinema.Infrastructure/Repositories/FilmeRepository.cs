using Cinema.Domain.Entidades;
using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Data.MongoDb.Documents;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cinema.Infrastructure.Repositories
{
    public class FilmeRepository(IMongoDatabase database) : IFilmeRepository
    {
        private readonly IMongoCollection<FilmeDocument> _collection = database.GetCollection<FilmeDocument>("Filmes");
        readonly Func<FilmeDocument, Filme> mapToDomain = filmeDocument => new Filme
        {
            Id = filmeDocument.Id!,
            Titulo = filmeDocument.Titulo,
            Diretor = filmeDocument.Diretor
        };

        public async Task<Filme> GetFilmeAsync(string id)
        {
            var filmeDocument = await _collection.Find(f => f.Id == id).FirstOrDefaultAsync();
            return mapToDomain(filmeDocument);
        }

        public async Task<IEnumerable<Filme>> GetFilmesAsync()
        {
            var filmeDocuments = await _collection.Find(f => true).ToListAsync();
            return filmeDocuments.Select(mapToDomain);
        }

        public async Task<IEnumerable<Filme>> GetFilmesAsync(IEnumerable<string> ids)
        {
            var filter = Builders<FilmeDocument>.Filter.In(f => f.Id, ids);
            var filmesDocument = await _collection.Find(filter).ToListAsync();

            return filmesDocument.Select(mapToDomain);
        }

        public async Task<IEnumerable<Filme>> GetFilmesByFilterAsync(string? diretor, string? titulo)
        {
            var builder = Builders<FilmeDocument>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(diretor))
            {
                filter &= builder.Regex(f => f.Diretor, new BsonRegularExpression(diretor, "i"));
            }

            if (!string.IsNullOrEmpty(titulo))
            {
                filter &= builder.Regex(f => f.Titulo, new BsonRegularExpression(titulo, "i"));
            }

            var projection = Builders<FilmeDocument>.Projection.Include(f => f.Id).Include(f => f.Titulo).Include(f => f.Diretor);

            var filmeDocuments = await _collection.Find(filter).Project<FilmeDocument>(projection).ToListAsync();
            return filmeDocuments.Select(mapToDomain);
        }
    }
}
