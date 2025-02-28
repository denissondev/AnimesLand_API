using AnimesLand.Domain.Entities;
using MediatR;

namespace AnimesLand.Application.Features.Animes.Queries
{
    public class GetAnimeByIdQuery : IRequest<Anime?>
    {
        public int Id { get; set; }

        public GetAnimeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
