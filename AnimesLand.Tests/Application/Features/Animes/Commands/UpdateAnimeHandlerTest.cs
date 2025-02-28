
using AnimesLand.Application.Features.Animes.Commands;
using AnimesLand.Domain.Entities;
using AnimesLand.Domain.Interfaces;
using Moq;

namespace AnimesLand.Tests.Application.Features.Animes.Commands
{
    public class UpdateAnimeCommandHandlerTests
    {
        private readonly Mock<IAnimeRepository> _animeRepositoryMock;
        private readonly UpdateAnimeCommandHandler _handler;

        public UpdateAnimeCommandHandlerTests()
        {
            _animeRepositoryMock = new Mock<IAnimeRepository>();
            _handler = new UpdateAnimeCommandHandler(_animeRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateAnime_WhenAnimeExists()
        {
            var anime = new Anime { Id = 1, Nome = "Naruto", Diretor = "Masashi Kishimoto", Resumo = "Metade do anime gritando 'Sasuke'" };
            var command = new UpdateAnimeCommand { Id = 1, Nome = "Naruto Shippuden", Diretor = "Masashi Kishimoto", Resumo = "Jogo da marionetes: Pain foi usado por Obito, que foi usado por Madara, que foi usado por Zetsu, que foi usado por Kaguya" };
            
            _animeRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id)).ReturnsAsync(anime);
            _animeRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Anime>())).Returns(Task.CompletedTask);
            
            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.True(result);
            Assert.Equal("Naruto Shippuden", anime.Nome);
            Assert.Equal("Masashi Kishimoto", anime.Diretor);
            Assert.Equal("Jogo da marionetes: Pain foi usado por Obito, que foi usado por Madara, que foi usado por Zetsu, que foi usado por Kaguya", anime.Resumo);
            
            _animeRepositoryMock.Verify(repo => repo.UpdateAsync(anime), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenAnimeDoesNotExist()
        {
            var command = new UpdateAnimeCommand { Id = 1, Nome = "Naruto Shippuden", Diretor = "Masashi Kishimoto", Resumo = "Jogo da marionetes: Pain foi usado por Obito, que foi usado por Madara, que foi usado por Zetsu, que foi usado por Kaguya" };
            
            _animeRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id)).ReturnsAsync((Anime?)null);
            
            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.False(result);
            _animeRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Anime>()), Times.Never);
        }
    }
}
