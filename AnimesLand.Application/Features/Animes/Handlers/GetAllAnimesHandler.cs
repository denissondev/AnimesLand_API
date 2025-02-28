using AnimesLand.Application.Features.Animes.Queries;
using AnimesLand.Domain.Entities;
using AnimesLand.Domain.Interfaces;
using MediatR;

namespace AnimesLand.Application.Features.Animes.Handlers
{
    public class GetAllAnimesHandler : IRequestHandler<GetAllAnimesQuery, IEnumerable<Anime>>
    {
        private readonly IAnimeRepository _animeRepository;

        public GetAllAnimesHandler(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<IEnumerable<Anime>> Handle(GetAllAnimesQuery request, CancellationToken cancellationToken)
        {
            return await _animeRepository.GetAllAsync();
        }
    }
}
