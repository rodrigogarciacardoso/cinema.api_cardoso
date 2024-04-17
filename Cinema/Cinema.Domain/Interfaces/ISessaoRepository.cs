using Cinema.Domain.Entidades;

namespace Cinema.Domain.Interfaces
{
    public interface ISessaoRepository
    {
        Task<Sessao> GetSessaoAsync(string id);
        Task<IEnumerable<Sessao>> GetSessoesBySalaAsync(string id);
        Task UpdateSessaoAsync(Sessao sessao);
        Task<IEnumerable<Sessao>> GetSessoesBySalasAsync(IEnumerable<string> ids);
    }
}