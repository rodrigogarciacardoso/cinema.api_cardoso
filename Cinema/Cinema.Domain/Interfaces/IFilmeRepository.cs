using Cinema.Domain.Entidades;

namespace Cinema.Domain.Interfaces
{
    public interface IFilmeRepository
    {
        Task<Filme> GetFilmeAsync(string id);
        Task<IEnumerable<Filme>> GetFilmesAsync();
        Task<IEnumerable<Filme>> GetFilmesByFilterAsync(string? diretor, string? titulo);
    }
}
