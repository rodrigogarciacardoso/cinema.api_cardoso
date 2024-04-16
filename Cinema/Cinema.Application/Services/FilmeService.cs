using Cinema.Application.Interfaces;
using Cinema.Domain.Entidades;
using Cinema.Domain.Interfaces;

namespace Cinema.Application.Services
{
    public class FilmeService(IFilmeRepository filmeRepository) : IFilmeService
    {
        private readonly IFilmeRepository _filmeRepository = filmeRepository;

        public async Task<Filme> GetFilmeAsync(string id)
        {
            return await _filmeRepository.GetFilmeAsync(id);
        }

        public async Task<IEnumerable<Filme>> GetFilmesByNomeOrDiretorAsync(string? titulo, string? diretor)
        {
            return await _filmeRepository.GetFilmesByFilterAsync(diretor, titulo);
        }
    }
}
