using AnimesLand.Domain.Interfaces;
using MediatR;

namespace AnimesLand.Application.Features.Animes.Commands
{
    public class DeleteAnimeCommandHandler : IRequestHandler<DeleteAnimeCommand, bool>
    {
        private readonly IAnimeRepository _animeRepository;

        public DeleteAnimeCommandHandler(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<bool> Handle(DeleteAnimeCommand request, CancellationToken cancellationToken)
        {
            var anime = await _animeRepository.GetByIdAsync(request.Id);
            if (anime == null)
                return false;

            await _animeRepository.DeleteAsync(anime);
            return true;
        }
    }
}