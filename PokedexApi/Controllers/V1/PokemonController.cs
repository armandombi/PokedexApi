using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokedexApi.Core.Interfaces;
using PokedexApi.Core.Models;
using Serilog;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace PokedexApi.Controllers.V1
{
    /// <summary>
    ///     Controller to handle all pokemon operations
    /// </summary>
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokedexService _pokedex;
        private readonly ITranslationService _translationService;

        /// <summary>
        /// Constructor to initialize all the services and dependencies used in the pokemon controller
        /// </summary>
        /// <param name="pokedex"></param>
        /// <param name="translationService"></param>
        public PokemonController(IPokedexService pokedex, ITranslationService translationService)
        {
            _pokedex = pokedex;
            _translationService = translationService;
        }

        /// <summary>
        /// Search for a pokemon based on the name and get its basic information
        /// </summary>
        /// <param name="name">The name of the pokemon</param>
        /// <response code="200">The pokemon details</response>
        /// <response code="404">The pokemon was not found</response>
        /// <response code="500">There is an issue retrieving the pokemon</response>
        /// <returns>The pokemon details</returns>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(PokemonDetails), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromRoute] string name)
        {
            try
            {
                var pokemonSpecies = await _pokedex.GetPokemonSpecies(name);
                if (pokemonSpecies == null)
                    return NotFound($"A pokemon by the name of {name} was not found");

                var pokemonDetails = new PokemonDetails(pokemonSpecies);
                return Ok(pokemonDetails);
            }
            catch (Exception exception)
            {
                Log.Error($"{MethodBase.GetCurrentMethod()?.DeclaringType} failed with exception {JsonConvert.SerializeObject(exception)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Search for a pokemon based on the name and get its basic information with a translated description
        /// </summary>
        /// <param name="name">The name of the pokemon</param>
        /// <response code="200">The pokemon details</response>
        /// <response code="404">The pokemon was not found</response>
        /// <response code="500">There is an issue retrieving the pokemon</response>
        /// <returns>The pokemon details containing the translated description, if the pokemon habitat is a cave or it is a legendary pokemon,
        /// a yoda translation is applied. For any other cases a shakespeare translation is returned</returns>
        [HttpGet("translated/{name}")]
        [ProducesResponseType(typeof(PokemonDetails), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTranslated([FromRoute] string name)
        {
            try
            {
                var pokemonSpecies = await _pokedex.GetPokemonSpecies(name);
                if (pokemonSpecies == null)
                    return NotFound($"A pokemon by the name of {name} was not found");

                var translatedPokemonDetails = await _translationService.GetTranslatedDetails(new PokemonDetails(pokemonSpecies));
                return Ok(translatedPokemonDetails);
            }
            catch (Exception exception)
            {
                Log.Error($"{MethodBase.GetCurrentMethod()?.DeclaringType} failed with exception {JsonConvert.SerializeObject(exception)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
