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
    }
}

