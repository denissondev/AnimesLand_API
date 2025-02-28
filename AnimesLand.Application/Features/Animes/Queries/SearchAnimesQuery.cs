using AnimesLand.Domain.Entities;
using MediatR;

namespace AnimesLand.Application.Features.Animes.Queries
{
    public class SearchAnimesQuery : IRequest<IEnumerable<Anime>>
    {
        public string? Nome { get; }
        public string? Diretor { get; }

        public SearchAnimesQuery(string? nome, string? diretor)
        {
            Nome = nome;
            Diretor = diretor;
        }
    }
}