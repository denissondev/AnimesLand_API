using AnimesLand.Application.Features.Animes.Handlers;
using AnimesLand.Application.Features.Animes.Queries;
using AnimesLand.Domain.Entities;
using AnimesLand.Domain.Interfaces;
using Moq;

namespace AnimesLand.Tests.Application.Features.Animes.Queries{
    public class SearchAnimesHandlerTests
    {
        private readonly Mock<IAnimeRepository> _animeRepositoryMock;
        private readonly SearchAnimesHandler _handler;

        public SearchAnimesHandlerTests()
        {
            _animeRepositoryMock = new Mock<IAnimeRepository>();
            _handler = new SearchAnimesHandler(_animeRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAnimes_WhenFilteredByName()
        {
            var nome = "Naruto";
            var animes = new List<Anime>
            {
                new Anime { Id = 1, Nome = nome, Diretor = "Masashi Kishimoto", Resumo = "Historia de Naruto Uzumaki" }
            };

            _animeRepositoryMock
                .Setup(repo => repo.SearchAsync(nome, null))
                .ReturnsAsync(animes);

            var query = new SearchAnimesQuery(nome, null);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(nome, result.First().Nome);
            _animeRepositoryMock.Verify(repo => repo.SearchAsync(nome, null), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnAnimes_WhenFilteredByDirector()
        {
            var diretor = "Eiichiro Oda";
            var animes = new List<Anime>
            {
                new Anime { Id = 2, Nome = "One Piece", Diretor = diretor, Resumo = "Pirata Monkey D. Luffy e sua tripulação" }
            };

            _animeRepositoryMock
                .Setup(repo => repo.SearchAsync(null, diretor))
                .ReturnsAsync(animes);

            var query = new SearchAnimesQuery(null, diretor);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(diretor, result.First().Diretor);
            _animeRepositoryMock.Verify(repo => repo.SearchAsync(null, diretor), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnAnimes_WhenFilteredByNameAndDirector()
        {
            var nome = "Naruto";
            var diretor = "Masashi Kishimoto";
            var animes = new List<Anime>
            {
                new Anime { Id = 1, Nome = nome, Diretor = diretor, Resumo = "Historia de Naruto Uzumaki" }
            };

            _animeRepositoryMock
                .Setup(repo => repo.SearchAsync(nome, diretor))
                .ReturnsAsync(animes);

            var query = new SearchAnimesQuery(nome, diretor);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(nome, result.First().Nome);
            Assert.Equal(diretor, result.First().Diretor);
            _animeRepositoryMock.Verify(repo => repo.SearchAsync(nome, diretor), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllAnimes_WhenNoFiltersProvided()
        {
            var animes = new List<Anime>
            {
                new Anime { Id = 1, Nome = "Naruto", Diretor = "Masashi Kishimoto", Resumo = "Historia de Naruto Uzumaki" },
                new Anime { Id = 2, Nome = "One Piece", Diretor = "Eiichiro Oda", Resumo = "Pirata Monkey D. Luffy" }
            };

            _animeRepositoryMock
                .Setup(repo => repo.SearchAsync(null, null))
                .ReturnsAsync(animes);

            var query = new SearchAnimesQuery(null, null);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _animeRepositoryMock.Verify(repo => repo.SearchAsync(null, null), Times.Once);
        }
    }
}
