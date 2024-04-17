using Cinema.Domain.Entidades;
using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Data.MongoDb.Documents;
using MongoDB.Driver;

namespace Cinema.Infrastructure.Repositories
{
    public class SalaRepository(IMongoDatabase database, ISessaoRepository sessaoRepository) : ISalaRepository
    {
        private readonly IMongoCollection<SalaDocument> _salas = database.GetCollection<SalaDocument>("Salas");
        private readonly ISessaoRepository _sessaoRepository = sessaoRepository;

        readonly Func<SalaDocument, IEnumerable<Sessao>, Sala> mapToDomain = (salaDocument, sessoes) =>
            new Sala(salaDocument.Id!, salaDocument.Numero, salaDocument.Capacidade, sessoes.ToList());

        readonly Func<Sala, SalaDocument> mapToDocument = (sala) => new SalaDocument
        {
            Id = sala.Id,
            Numero = sala.Numero,
            Capacidade = sala.Capacidade
        };

        public async Task<Sala> GetSalaAsync(string id)
        {
            var salaDocument = await _salas.Find(s => s.Id == id).FirstOrDefaultAsync();
            var sessoes = await _sessaoRepository.GetSessoesBySalaAsync(id);

            return mapToDomain(salaDocument, sessoes);
        }

        public async Task<IEnumerable<Sala>> GetSalasAsync(IEnumerable<string> ids)
        {
            var filter = Builders<SalaDocument>.Filter.In(s => s.Id, ids);
            var salasDocument = await _salas.Find(filter).ToListAsync();

            var sessoes = await _sessaoRepository.GetSessoesBySalasAsync(ids);

            return salasDocument.Select(s => mapToDomain(s, sessoes.Where(sessao => sessao.Sala.Id == s.Id)));
        }

        public async Task UpdateSalaAsync(Sala sala)
        {
            var salaDocument = mapToDocument(sala);

            await _salas.ReplaceOneAsync(s => s.Id == salaDocument.Id, salaDocument);
        }
    }
}