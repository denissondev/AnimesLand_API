using AnimesLand.Api.Controllers;
using AnimesLand.Application.Features.Animes.Commands;
using AnimesLand.Application.Features.Animes.Queries;
using AnimesLand.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AnimesLand.Api.Tests.Controllers
{
    public class AnimesControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AnimesController _controller;

        public AnimesControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new AnimesController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllAnimes_ReturnsOk_WhenAnimesExist()
        {
            // Arrange
            var animes = new List<Anime>
            {
                new Anime { Id = 1, Nome = "Naruto" },
                new Anime { Id = 2, Nome = "One Piece" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllAnimesQuery>(), default))
                        .ReturnsAsync(animes);

            // Act
            var result = await _controller.GetAllAnimes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Anime>>(okResult.Value);
            Assert.Equal(animes.Count, returnValue.Count);
        }

        [Fact]
        public async Task GetAllAnimes_ReturnsNotFound_WhenNoAnimesExist()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllAnimesQuery>(), default))
             .ReturnsAsync(new List<Anime>());

            var result = await _controller.GetAllAnimes();

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetAnimeById_ReturnsOk_WhenAnimeExists()
        {
            var anime = new Anime
            {
                Id = 1,
                Nome = "Naruto",
                Diretor = "Masashi Kishimoto",
                Resumo = "Metade do anime gritando 'Sasuke'."
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAnimeByIdQuery>(), default))
                        .ReturnsAsync(anime);

            var result = await _controller.GetAnimeById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Anime>(okResult.Value);
            Assert.Equal(anime.Nome, returnValue.Nome); 
            Assert.Equal(anime.Diretor, returnValue.Diretor);
            Assert.Equal(anime.Resumo, returnValue.Resumo); 
        }

        [Fact]
        public async Task GetAnimeById_ReturnsNotFound_WhenAnimeDoesNotExist()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAnimeByIdQuery>(), default))
                        .ReturnsAsync((Anime)null);

            var result = await _controller.GetAnimeById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateAnime_ReturnsCreatedAtAction_WhenSuccessful()
        {
            var animeId = 1;
            var command = new CreateAnimeCommand();
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateAnimeCommand>(), default)).ReturnsAsync(animeId);

            var result = await _controller.CreateAnime(command);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetAnimeById", createdAtActionResult.ActionName);
            Assert.Equal(animeId, createdAtActionResult.RouteValues!["id"]);
        }

        [Fact]
        public async Task UpdateAnime_ReturnsNoContent_WhenSuccessful()
        {
            var command = new UpdateAnimeCommand { Id = 1 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateAnimeCommand>(), default)).ReturnsAsync(true);

            var result = await _controller.UpdateAnime(1, command);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAnime_ReturnsNoContent_WhenSuccessful()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteAnimeCommand>(), default)).ReturnsAsync(true);

            var result = await _controller.DeleteAnime(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
