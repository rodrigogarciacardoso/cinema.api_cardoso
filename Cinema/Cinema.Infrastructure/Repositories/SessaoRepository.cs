using Cinema.Domain.Entidades;
using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Data.MongoDb.Documents;
using MongoDB.Driver;

namespace Cinema.Infrastructure.Repositories
{
    public class SessaoRepository(IMongoDatabase database, IFilmeRepository filmeRepository, ISalaRepository salaRepository) : ISessaoRepository
    {
        private readonly IMongoCollection<SessaoDocument> _sessoes = database.GetCollection<SessaoDocument>("Sessoes");
        private readonly IFilmeRepository _filmeRepository = filmeRepository;
        private readonly ISalaRepository _salaRepository = salaRepository;

        readonly Func<SessaoDocument, Filme, Sala, Sessao> mapToDomain = (sessaoDocument, filme, sala) =>
            new Sessao(sessaoDocument.Id!, filme, sessaoDocument.HorarioInicio, sessaoDocument.HorarioFim, sala, sessaoDocument.IngressosDisponiveis);

        readonly Func<Sessao, SessaoDocument> mapToDocument = (sessao) => new SessaoDocument
        {
            Id = sessao.Id,
            FilmeId = sessao.Filme.Id,
            SalaId = sessao.Sala.Id,
            HorarioInicio = sessao.HorarioInicio,
            HorarioFim = sessao.HorarioFim,
            IngressosDisponiveis = sessao.IngressosDisponiveis
        };

        public async Task<Sessao> GetSessaoAsync(string id)
        {
            var sessaoDocument = await _sessoes.Find(s => s.Id == id).FirstOrDefaultAsync();

            var filme = await _filmeRepository.GetFilmeAsync(sessaoDocument.FilmeId);

            var sala = await _salaRepository.GetSalaAsync(sessaoDocument.SalaId);

            return mapToDomain(sessaoDocument, filme, sala);
        }

        public async Task<IEnumerable<Sessao>> GetSessoesBySalaAsync(string id)
        {
            var sessoesDocument = await _sessoes.Find(s => s.SalaId == id).ToListAsync();

            var filmeIds = sessoesDocument.Select(s => s.FilmeId).Distinct().ToList();
            var salaIds = sessoesDocument.Select(s => s.SalaId).Distinct().ToList();

            var filmes = await _filmeRepository.GetFilmesAsync(filmeIds);
            var salas = await _salaRepository.GetSalasAsync(salaIds);

            var filmeDictionary = filmes.ToDictionary(f => f.Id);
            var salaDictionary = salas.ToDictionary(s => s.Id);

            return sessoesDocument.Select(s => mapToDomain(s, filmeDictionary[s.FilmeId], salaDictionary[s.SalaId]));
        }

        public async Task<IEnumerable<Sessao>> GetSessoesBySalasAsync(IEnumerable<string> ids)
        {
            var filter = Builders<SessaoDocument>.Filter.In(s => s.SalaId, ids);
            var sessoesDocument = await _sessoes.Find(filter).ToListAsync();

            var filmeIds = sessoesDocument.Select(s => s.FilmeId).Distinct().ToList();
            var salaIds = sessoesDocument.Select(s => s.SalaId).Distinct().ToList();

            var filmes = await _filmeRepository.GetFilmesAsync(filmeIds);
            var salas = await _salaRepository.GetSalasAsync(salaIds);

            var filmeDictionary = filmes.ToDictionary(f => f.Id);
            var salaDictionary = salas.ToDictionary(s => s.Id);

            return sessoesDocument.Select(s => mapToDomain(s, filmeDictionary[s.FilmeId], salaDictionary[s.SalaId]));
        }

        public async Task<IEnumerable<Sessao>> GetSessoesAsync(IEnumerable<string> ids)
        {
            var filter = Builders<SessaoDocument>.Filter.In(s => s.Id, ids);
            var sessoesDocument = await _sessoes.Find(filter).ToListAsync();

            var filmeIds = sessoesDocument.Select(s => s.FilmeId).Distinct().ToList();
            var salaIds = sessoesDocument.Select(s => s.SalaId).Distinct().ToList();

            var filmes = await _filmeRepository.GetFilmesAsync(filmeIds);
            var salas = await _salaRepository.GetSalasAsync(salaIds);

            var filmeDictionary = filmes.ToDictionary(f => f.Id);
            var salaDictionary = salas.ToDictionary(s => s.Id);

            return sessoesDocument.Select(s => mapToDomain(s, filmeDictionary[s.FilmeId], salaDictionary[s.SalaId]));
        }

        public async Task UpdateSessaoAsync(Sessao sessao)
        {
            var sessaoDocument = mapToDocument(sessao);
            await _sessoes.ReplaceOneAsync(s => s.Id == sessaoDocument.Id, sessaoDocument);
        }
    }
}