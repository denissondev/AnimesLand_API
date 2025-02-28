using MediatR;

namespace AnimesLand.Application.Features.Animes.Commands
{
    public class DeleteAnimeCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteAnimeCommand(int id)
        {
            Id = id;
        }
    }
}
