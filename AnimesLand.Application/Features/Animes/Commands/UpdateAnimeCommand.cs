using MediatR;

namespace AnimesLand.Application.Features.Animes.Commands
{
    public class UpdateAnimeCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Diretor { get; set; } = string.Empty;
        public string Resumo { get; set; } = string.Empty;
    }
}
