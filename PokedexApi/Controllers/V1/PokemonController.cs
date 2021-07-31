using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokedexApi.Core.Interfaces;
using PokedexApi.Core.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Search for a pokemon based on the name and return basic pokemon information
        /// </summary>
        /// <param name="pokemonName">The name of the pokemon</param>
        /// <response code="200">The pokemon details</response>
        /// <response code="404">The pokemon was not found</response>
        /// <response code="500">There is an issue retrieving the pokemon</response>
        /// <returns>The pokemon details</returns>
        [HttpGet("{pokemonName}")]
        [ProducesResponseType(typeof(PokemonDetails), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromRoute] string pokemonName)
        {
            try
            {
                var pokemonSpecies = await _pokedex.GetPokemonSpeciesAsync(pokemonName);
                if (pokemonSpecies == null)
                    return NotFound($"A pokemon by the name of {pokemonName} was not found");

                var pokemonDetails = new PokemonDetails(pokemonSpecies);
                return Ok(pokemonDetails);
            }
            catch (Exception exception)
            {
                Log.Error($"{MethodBase.GetCurrentMethod()?.DeclaringType} failed with exception {JsonConvert.SerializeObject(exception)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
