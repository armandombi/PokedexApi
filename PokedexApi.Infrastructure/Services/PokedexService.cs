using Newtonsoft.Json;
using PokedexApi.Core.Interfaces;
using PokedexApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokedexApi.Infrastructure.Services
{
    public class PokedexService : IPokedexService
    {
        private readonly HttpClient _httpClient;
        public PokedexService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<PokemonSpecies> GetPokemonSpeciesAsync<T>(T identifier)
        {
            if (!IsAllowedType(typeof(T)))
                throw new NotSupportedException("The identifier provided is not supported");

            var response = await _httpClient.GetAsync($"pokemon-species/{identifier}");

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<PokemonSpecies>(await response.Content.ReadAsStringAsync());
        }

        private static bool IsAllowedType(Type itemType)
        {
            return itemType == typeof(int) || itemType == typeof(string);
        }
    }
}
