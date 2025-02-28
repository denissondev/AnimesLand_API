using AnimesLand.Domain.Interfaces;
using MediatR;

namespace AnimesLand.Application.Features.Animes.Commands
{
    public class UpdateAnimeCommandHandler : IRequestHandler<UpdateAnimeCommand, bool>
    {
        private readonly IAnimeRepository _animeRepository;

        public UpdateAnimeCommandHandler(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<bool> Handle(UpdateAnimeCommand request, CancellationToken cancellationToken)
        {
            var anime = await _animeRepository.GetByIdAsync(request.Id);
            if (anime == null)
                return false;

            anime.Nome = request.Nome;
            anime.Diretor = request.Diretor;
            anime.Resumo = request.Resumo;

            await _animeRepository.UpdateAsync(anime);
            return true;
        }
    }
}
