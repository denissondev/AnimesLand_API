using MediatR;

namespace AnimesLand.Application.Features.Animes.Commands
{
    public class CreateAnimeCommand : IRequest<int>
    {
        public string Nome { get; set; } = string.Empty;
        public string Diretor { get; set; } = string.Empty;
        public string Resumo { get; set; } = string.Empty;
    }
}
