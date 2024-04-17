using Cinema.Application.Interfaces;
using Cinema.Domain.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace cinema.api_cardoso.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmesController(IFilmeService filmeService) : ControllerBase
    {
        private readonly IFilmeService _filmeService = filmeService;

        [HttpGet("{id}")]
        public async Task<ActionResult<Filme>> GetFilme(string id)
        {
            var filme = await _filmeService.GetFilmeAsync(id);
            if (filme == null)
            {
                return NotFound();
            }
            return filme;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Filme>>> GetFilmesByFilter([FromQuery] string? titulo, [FromQuery] string? diretor)
        {
            var filmes = await _filmeService.GetFilmesByNomeOrDiretorAsync(titulo, diretor);
            if (filmes == null || !filmes.Any())
            {
                return NotFound();
            }
            return Ok(filmes);
        }
    }
}

