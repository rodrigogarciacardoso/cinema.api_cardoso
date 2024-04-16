using Cinema.Application.Services;
using Cinema.Domain.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace cinema.api_cardoso.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmesController(FilmeService filmeService) : ControllerBase
    {
        private readonly FilmeService _filmeService = filmeService;

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
    }
}

