using AnimesLand.Application.Features.Animes.Queries;
using AnimesLand.Domain.Entities;
using AnimesLand.Domain.Interfaces;
using MediatR;

namespace AnimesLand.Application.Features.Animes.Handlers
{
    public class GetAnimeByIdHandler : IRequestHandler<GetAnimeByIdQuery, Anime?>
    {
        private readonly IAnimeRepository _animeRepository;

        public GetAnimeByIdHandler(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<Anime?> Handle(GetAnimeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _animeRepository.GetByIdAsync(request.Id);
        }
    }
}
