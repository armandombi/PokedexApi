using Newtonsoft.Json;
using PokedexApi.Core.Interfaces;
using PokedexApi.Core.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokedexApi.Infrastructure.Services
{
    /// <summary>
    /// Service to handle all request to the pokeApi
    /// </summary>
    public class PokedexService : IPokedexService
    {
        private readonly HttpClient _httpClient;
        public PokedexService(HttpClient client)
        {
            _httpClient = client;
        }

        /// <summary>
        /// Retrieve the pokemon species information from the pokeApi
        /// </summary>
        /// <typeparam name="T">The type of identifier to request the information. Allowed values are string or integer</typeparam>
        /// <param name="identifier">The identifier of the pokemon, either the name or the id</param>
        /// <returns>The requested pokemon species details</returns>
        public async Task<PokemonSpecies> GetPokemonSpecies<T>(T identifier)
        {
            if (!IsAllowedType(typeof(T)))
                throw new NotSupportedException("The identifier provided is not supported");

            var response = await _httpClient.GetAsync($"pokemon-species/{identifier}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<PokemonSpecies>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Validates if the passed type is allowed
        /// </summary>
        /// <param name="itemType">The type to validate</param>
        /// <returns>True if it is a valid type and false if it is not</returns>
        private static bool IsAllowedType(Type itemType)
        {
            return itemType == typeof(int) || itemType == typeof(string);
        }
    }
}
