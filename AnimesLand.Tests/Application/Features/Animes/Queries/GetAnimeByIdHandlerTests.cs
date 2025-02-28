using AnimesLand.Application.Features.Animes.Handlers;
using AnimesLand.Application.Features.Animes.Queries;
using AnimesLand.Domain.Entities;
using AnimesLand.Domain.Interfaces;
using Moq;

namespace AnimesLand.Tests.Application.Features.Animes.Queries{
    public class GetAnimeByIdHandlerTests
    {
        private readonly Mock<IAnimeRepository> _animeRepositoryMock;
        private readonly GetAnimeByIdHandler _handler;

        public GetAnimeByIdHandlerTests()
        {
            _animeRepositoryMock = new Mock<IAnimeRepository>();
            _handler = new GetAnimeByIdHandler(_animeRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAnime_WhenAnimeExists()
        {
            // Arrange
            var animeId = 1;
            var anime = new Anime
            {
                Id = animeId,
                Nome = "Attack on Titan",
                Diretor = "Hajime Isayama",
                Resumo = "Humanos contra Titãs em muralhas gigantes"
            };

            _animeRepositoryMock
                .Setup(repo => repo.GetByIdAsync(animeId))
                .ReturnsAsync(anime);

            var query = new GetAnimeByIdQuery(animeId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(animeId, result.Id);
            Assert.Equal(anime.Nome, result.Nome);
            Assert.Equal(anime.Diretor, result.Diretor);
            Assert.Equal(anime.Resumo, result.Resumo);

            _animeRepositoryMock.Verify(repo => repo.GetByIdAsync(animeId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAnimeDoesNotExist()
        {
            // Arrange
            var animeId = 2;

            _animeRepositoryMock
                .Setup(repo => repo.GetByIdAsync(animeId))
                .ReturnsAsync((Anime?)null);

            var query = new GetAnimeByIdQuery(animeId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);

            _animeRepositoryMock.Verify(repo => repo.GetByIdAsync(animeId), Times.Once);
        }
    }
}


