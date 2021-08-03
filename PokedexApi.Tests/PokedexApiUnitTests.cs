using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PokedexApi.Controllers.V1;
using PokedexApi.Core.Interfaces;
using PokedexApi.Core.Models;
using PokedexApi.Tests.Helper;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PokedexApi.Tests
{
    public class PokedexApiUnitTests
    {
        private readonly Mock<IPokedexService> _mockPokedexService;
        private readonly Mock<ITranslationService> _mockTranslationService;
        private readonly PokemonController _mockController;

        public PokedexApiUnitTests()
        {
            _mockPokedexService = new Mock<IPokedexService>();
            _mockTranslationService = new Mock<ITranslationService>();
            _mockController = new PokemonController(_mockPokedexService.Object, _mockTranslationService.Object);
        }

        [Fact]
        public async Task Get_Pokemon_Details_Success()
        {
            var pokemonSpecies = PokemonDataGenerator.GenerateValidPokemonSpecies("pikachu", "forest", false);
            _mockPokedexService
                .Setup(x => x.GetPokemonSpecies(It.Is<string>(s => s == pokemonSpecies.Name)))
                .ReturnsAsync(pokemonSpecies);

            var response = await _mockController.Get(pokemonSpecies.Name);

            response.As<ObjectResult>()?.Should().BeOfType<OkObjectResult>();
            response.As<ObjectResult>()?.Value.Should().BeOfType<PokemonDetails>();
            response.As<ObjectResult>()?.Value.As<PokemonDetails>().Name.Should().Be(pokemonSpecies.Name);
            response.As<ObjectResult>()?.Value.As<PokemonDetails>().Description.Should().Be(pokemonSpecies.FlavorText.Find(x => x.Language.Name == "en").Text);
            response.As<ObjectResult>()?.Value.As<PokemonDetails>().Habitat.Should().Be(pokemonSpecies.Habitat.Name);
        }

        [Fact]
        public async Task Get_Pokemon_Details_NotFound()
        {
            var pokemonSpecies = PokemonDataGenerator.GenerateValidPokemonSpecies("pikachu", "forest", false);
            _mockPokedexService
                .Setup(x => x.GetPokemonSpecies(It.Is<string>(s => s == pokemonSpecies.Name)))
                .ReturnsAsync(pokemonSpecies);

            var response = await _mockController.Get(default);

            response.As<NotFoundObjectResult>()?.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Get_Pokemon_Details_Failed()
        {
            _mockPokedexService
                .Setup(x => x.GetPokemonSpecies(It.IsAny<string>()))
                .Throws<InvalidOperationException>();

            var response = await _mockController.Get(default);

            response.As<ObjectResult>()?.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task GetTranslated_Pokemon_Details_Success()
        {
            var pokemonSpecies = PokemonDataGenerator.GenerateValidPokemonSpecies("ditto", "urban", false);
            _mockPokedexService
                .Setup(x => x.GetPokemonSpecies(It.Is<string>(s => s == pokemonSpecies.Name)))
                .ReturnsAsync(pokemonSpecies);

            _mockTranslationService
                .Setup(x => x.GetTranslatedDetails(It.IsAny<PokemonDetails>()))
                .ReturnsAsync(new PokemonDetails(pokemonSpecies));


            var response = await _mockController.GetTranslated(pokemonSpecies.Name);

            response.As<ObjectResult>()?.Should().BeOfType<OkObjectResult>();
            response.As<ObjectResult>()?.Value.Should().BeOfType<PokemonDetails>();
            response.As<ObjectResult>()?.Value.As<PokemonDetails>().Name.Should().Be(pokemonSpecies.Name);
            response.As<ObjectResult>()?.Value.As<PokemonDetails>().Description.Should().NotBeNullOrEmpty();
            response.As<ObjectResult>()?.Value.As<PokemonDetails>().Habitat.Should().Be(pokemonSpecies.Habitat.Name);
        }

        [Fact]
        public async Task GetTranslated_Pokemon_Details_NotFound()
        {
            var pokemonSpecies = PokemonDataGenerator.GenerateValidPokemonSpecies("ditto", "urban", false);
            _mockPokedexService
                .Setup(x => x.GetPokemonSpecies(It.Is<string>(s => s == pokemonSpecies.Name)))
                .ReturnsAsync(pokemonSpecies);

            var response = await _mockController.GetTranslated(default);

            response.As<NotFoundObjectResult>()?.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
