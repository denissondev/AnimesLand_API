using AnimesLand.Domain.Entities;
using AnimesLand.Domain.Interfaces;
using MediatR;

namespace AnimesLand.Application.Features.Animes.Commands
{
    public class CreateAnimeCommandHandler : IRequestHandler<CreateAnimeCommand, int>
    {
        private readonly IAnimeRepository _animeRepository;

        public CreateAnimeCommandHandler(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<int> Handle(CreateAnimeCommand request, CancellationToken cancellationToken)
        {
            var anime = new Anime
            {
                Nome = request.Nome,
                Diretor = request.Diretor,
                Resumo = request.Resumo
            };

            await _animeRepository.AddAsync(anime);
            return anime.Id;
        }
    }
}
