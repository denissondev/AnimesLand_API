using AnimesLand.Application.Features.Animes.Commands;
using AnimesLand.Domain.Entities;
using AnimesLand.Domain.Interfaces;
using Moq;

namespace AnimesLand.Tests.Application.Features.Animes.Commands
{
    public class DeleteAnimeCommandHandlerTests
    {
        private readonly Mock<IAnimeRepository> _animeRepositoryMock;
        private readonly DeleteAnimeCommandHandler _handler;

        public DeleteAnimeCommandHandlerTests()
        {
            _animeRepositoryMock = new Mock<IAnimeRepository>();
            _handler = new DeleteAnimeCommandHandler(_animeRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenAnimeExistsAndIsDeleted()
        {
            var animeId = 1;
            var anime = new Anime { Id = animeId, Nome = "Naruto" };

            _animeRepositoryMock
                .Setup(repo => repo.GetByIdAsync(animeId))
                .ReturnsAsync(anime);

            _animeRepositoryMock
                .Setup(repo => repo.DeleteAsync(anime))
                .Returns(Task.CompletedTask);

            var command = new DeleteAnimeCommand (animeId);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result);
            _animeRepositoryMock.Verify(repo => repo.GetByIdAsync(animeId), Times.Once);
            _animeRepositoryMock.Verify(repo => repo.DeleteAsync(anime), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenAnimeDoesNotExist()
        {
            var animeId = 1;

            _animeRepositoryMock
                .Setup(repo => repo.GetByIdAsync(animeId))
                .ReturnsAsync((Anime?)null); 

            var command = new DeleteAnimeCommand(animeId);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result); 
            _animeRepositoryMock.Verify(repo => repo.GetByIdAsync(animeId), Times.Once); 
            _animeRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Anime>()), Times.Never); 
        }
    }
}