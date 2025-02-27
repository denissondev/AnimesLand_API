using AnimesLand.Application.Features.Animes.Commands;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AnimesLand.Application.Features.Animes.Queries;

namespace AnimesLand.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnimesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnimes()
        {
            var query = new GetAllAnimesQuery();
            var animes = await _mediator.Send(query);

            if (animes == null || !animes.Any())
                return NotFound("Nenhum anime encontrado.");

            return Ok(animes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimeById(int id)
        {
            var query = new GetAnimeByIdQuery(id);
            var anime = await _mediator.Send(query);

            if (anime == null)
                return NotFound();

            return Ok(anime);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAnimes([FromQuery] string? nome, [FromQuery] string? diretor)
        {
            if (string.IsNullOrWhiteSpace(nome) && string.IsNullOrWhiteSpace(diretor))
                return BadRequest("Pelo menos um critério de pesquisa (Nome ou Diretor) deve ser fornecido.");

            var query = new SearchAnimesQuery(nome, diretor);
            var animes = await _mediator.Send(query);

            if (animes == null || !animes.Any())
                return NotFound("Nenhum anime encontrado com os critérios fornecidos.");

            return Ok(animes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnime([FromBody] CreateAnimeCommand command)
        {
            var animeId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAnimeById), new { id = animeId }, animeId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnime(int id, [FromBody] UpdateAnimeCommand command)
        {
            if (id != command.Id)
                return BadRequest("O ID na URL deve ser igual ao ID no corpo da requisição.");

            var result = await _mediator.Send(command);

            if (!result)
                return NotFound("Anime não encontrado.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnime(int id)
        {
            var result = await _mediator.Send(new DeleteAnimeCommand(id));

            if (!result)
                return NotFound("Anime não encontrado.");

            return NoContent();
        }
    }
}
