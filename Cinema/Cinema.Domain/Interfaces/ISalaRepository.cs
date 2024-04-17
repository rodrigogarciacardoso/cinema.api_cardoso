using Cinema.Domain.Entidades;

namespace Cinema.Domain.Interfaces
{
    public interface ISalaRepository
    {
        Task<Sala> GetSalaAsync(string id);
        Task UpdateSalaAsync(Sala sala);
        Task<IEnumerable<Sala>> GetSalasAsync(IEnumerable<string> ids);
    }
}