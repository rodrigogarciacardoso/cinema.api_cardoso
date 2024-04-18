using Cinema.Domain.Entidades;

namespace Cinema.Domain.Interfaces
{
    public interface IIngressoRepository
    {
        Task<Ingresso> GetIngressoAsync(string id);
        Task UpdateIngressoAsync(Ingresso ingresso);
        Task<IEnumerable<Ingresso>> GetIngressosPorClienteAsync(string id);
    }
}