using AnimesLand.Application.Features.Animes.Handlers;
using AnimesLand.Application.Features.Animes.Queries;
using AnimesLand.Domain.Entities;
using AnimesLand.Domain.Interfaces;
using Moq;

namespace AnimesLand.Tests.Application.Features.Animes.Queries{
    public class GetAllAnimesHandlerTests
    {
        private readonly Mock<IAnimeRepository> _animeRepositoryMock;
        private readonly GetAllAnimesHandler _handler;

        public GetAllAnimesHandlerTests()
        {
            _animeRepositoryMock = new Mock<IAnimeRepository>();
            _handler = new GetAllAnimesHandler(_animeRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllAnimes_WhenAnimesExist()
        {
            var animes = new List<Anime>
            {
                new Anime { Id = 1, Nome = "Naruto", Diretor = "Masashi Kishimoto", Resumo = "Ninja que quer ser Hokage" },
                new Anime { Id = 2, Nome = "Dragon Ball Z", Diretor = "Akira Toriyama", Resumo = "Guerreiros lutando pelo universo" }
            };

            _animeRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(animes);

            var query = new GetAllAnimesQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(animes.Count, result.Count());
            Assert.Equal(animes, result);
            _animeRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoAnimesExist()
        {
            _animeRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Anime>());

            var query = new GetAllAnimesQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Empty(result);
            _animeRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
    }
}

