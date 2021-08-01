using Newtonsoft.Json;
using PokedexApi.Core.Interfaces;
using PokedexApi.Core.Models;
using System;
using System.Net.Http;
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

        private static bool IsAllowedType(Type itemType)
        {
            return itemType == typeof(int) || itemType == typeof(string);
        }
    }
}
