using AnimesLand.Application.Features.Animes.Queries;
using AnimesLand.Domain.Entities;
using AnimesLand.Domain.Interfaces;
using MediatR;

namespace AnimesLand.Application.Features.Animes.Handlers
{
    public class SearchAnimesHandler : IRequestHandler<SearchAnimesQuery, IEnumerable<Anime>>
    {
        private readonly IAnimeRepository _animeRepository;

        public SearchAnimesHandler(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<IEnumerable<Anime>> Handle(SearchAnimesQuery request, CancellationToken cancellationToken)
        {
            return await _animeRepository.SearchAsync(request.Nome, request.Diretor);
        }
    }
}
