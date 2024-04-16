using Cinema.Domain.Entidades;

namespace Cinema.Application.Interfaces
{
    public interface IFilmeService
    {
        Task<Filme> GetFilmeAsync(string id);
        Task<IEnumerable<Filme>> GetFilmesByNomeOrDiretorAsync(string? titulo, string? diretor);
    }
}
