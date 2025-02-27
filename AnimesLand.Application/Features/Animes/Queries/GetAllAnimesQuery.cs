using AnimesLand.Domain.Entities;
using MediatR;

namespace AnimesLand.Application.Features.Animes.Queries
{
    public class GetAllAnimesQuery : IRequest<IEnumerable<Anime>>
    {
    }
}
