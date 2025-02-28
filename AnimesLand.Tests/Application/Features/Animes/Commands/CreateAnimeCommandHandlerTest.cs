using Moq;
using AnimesLand.Application.Features.Animes.Commands;
using AnimesLand.Domain.Entities;
using AnimesLand.Domain.Interfaces;

namespace AnimesLand.Tests.Application.Features.Animes.Commands
{
    public class CreateAnimeCommandHandlerTests
    {
        private readonly Mock<IAnimeRepository> _animeRepositoryMock;
        private readonly CreateAnimeCommandHandler _handler;

        public CreateAnimeCommandHandlerTests()
        {
            _animeRepositoryMock = new Mock<IAnimeRepository>();            
            _handler = new CreateAnimeCommandHandler(_animeRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAnimeId_WhenAnimeIsCreated()
        {
            // Arrange
            var command = new CreateAnimeCommand
            {
                Nome = "Naruto",
                Diretor = "Masashi Kishimoto",
                Resumo = "História das marionetes: Obito usado por Madara, Madara usado por Zetsu, Zetsu usado por Kaguya"
            };

            var anime = new Anime
            {
                Id = 1,
                Nome = command.Nome,
                Diretor = command.Diretor,
                Resumo = command.Resumo
            };

            _animeRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Anime>()))
                .Callback<Anime>(a => a.Id = anime.Id) 
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Equal(anime.Id, result);

            _animeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Anime>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCallAddAsyncOnce_WhenAnimeIsCreated()
        {
            // Arrange
            var command = new CreateAnimeCommand
            {
                Nome = "Dragon Ball Z",
                Diretor = "Akira Toriyama",
                Resumo = "Anime que precisa de 5 temporadas para contaro que aconteceu em um dia"
            };

            _animeRepositoryMock
                .Setup(repo => repo .AddAsync(It.IsAny<Anime>()))
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            _animeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Anime>()), Times.Once);
        }
    }
}
