using Cinema.Domain.Entidades;

namespace Cinema.Application.Interfaces
{
    public interface IFilmeService
    {
        Task<Filme> GetFilmeAsync(string id);
    }
}
