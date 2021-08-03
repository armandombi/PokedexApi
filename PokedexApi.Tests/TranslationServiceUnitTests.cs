using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PokedexApi.Core.Interfaces;
using PokedexApi.Core.Models;
using PokedexApi.Tests.Helper;
using System.Threading.Tasks;
using Xunit;

namespace PokedexApi.Tests
{
    public class TranslationServiceUnitTests
    {
        private readonly ITranslationService _mockTranslationService;
        public TranslationServiceUnitTests()
        {
            var mockTranslationService = new Mock<ITranslationService>();
            mockTranslationService
                .Setup(x => x.GetShakespeareTranslation(It.IsAny<string>()))
                .ReturnsAsync("ShakespeareTranslation");

            mockTranslationService
                .Setup(x => x.GetYodaTranslation(It.IsAny<string>()))
                .ReturnsAsync("YodaTranslation");
            _mockTranslationService = mockTranslationService.Object;
        }

        [Fact]
        public async Task Get_Translated_Details_Success()
        {
            var pokemonSpecies = PokemonDataGenerator.GenerateValidPokemonSpecies(default, default, false);
            var response = await _mockTranslationService.GetTranslatedDetails(new PokemonDetails(pokemonSpecies));

            response.As<ObjectResult>()?.Should().BeOfType<OkObjectResult>();
            response.As<ObjectResult>()?.Value.Should().BeOfType<PokemonDetails>();
        }

        [Fact]
        public async Task Get_Translated_Details_ShakespeareTranslation_Success()
        {
            var pokemonSpecies = PokemonDataGenerator.GenerateValidPokemonSpecies("pikachu", "forest", false);
            var response = await _mockTranslationService.GetTranslatedDetails(new PokemonDetails(pokemonSpecies));

            response.As<ObjectResult>()?.Should().BeOfType<OkObjectResult>();
            response.As<ObjectResult>()?.Value.Should().BeOfType<PokemonDetails>();
            response.As<ObjectResult>()?.Value.As<PokemonDetails>().Name.Should().Be(pokemonSpecies.Name);
            response.As<ObjectResult>()?.Value.As<PokemonDetails>().Description.Should().Be("ShakespeareTranslation");
            response.As<ObjectResult>()?.Value.As<PokemonDetails>().Habitat.Should().Be(pokemonSpecies.Habitat.Name);
        }

        [Fact]
        public async Task Get_Translated_Details_YodaTranslation_cave_Success()
        {
            var pokemonSpecies = PokemonDataGenerator.GenerateValidPokemonSpecies("pokeName", "cave", false);
            var response = await _mockTranslationService.GetTranslatedDetails(new PokemonDetails(pokemonSpecies));

            response.As<ObjectResult>()?.Should().BeOfType<OkObjectResult>();
            response.As<ObjectResult>()?.Value.Should().BeOfType<PokemonDetails>();
            response.As<ObjectResult>()?.Value.As<PokemonDetails>().Name.Should().Be(pokemonSpecies.Name);
            response.As<ObjectResult>()?.Value.As<PokemonDetails>().Description.Should().Be("YodaTranslation");
            response.As<ObjectResult>()?.Value.As<PokemonDetails>().Habitat.Should().Be(pokemonSpecies.Habitat.Name);
        }

        [Fact]
        public async Task Get_Translated_Details_YodaTranslation_IsLegendary_Success()
        {
            var pokemonSpecies = PokemonDataGenerator.GenerateValidPokemonSpecies("pokeName", "urban", true);
            var response = await _mockTranslationService.GetTranslatedDetails(new PokemonDetails(pokemonSpecies));

            response.As<ObjectResult>()?.Should().BeOfType<OkObjectResult>();
            response.As<ObjectResult>()?.Value.Should().BeOfType<PokemonDetails>();
            response.As<ObjectResult>()?.Value.As<PokemonDetails>().Name.Should().Be(pokemonSpecies.Name);
            response.As<ObjectResult>()?.Value.As<PokemonDetails>().Description.Should().Be("YodaTranslation");
            response.As<ObjectResult>()?.Value.As<PokemonDetails>().Habitat.Should().Be(pokemonSpecies.Habitat.Name);
        }
    }
}
