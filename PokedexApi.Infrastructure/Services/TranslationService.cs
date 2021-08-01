using Newtonsoft.Json;
using PokedexApi.Core.Interfaces;
using PokedexApi.Core.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PokedexApi.Infrastructure.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly HttpClient _httpClient;

        public TranslationService(HttpClient client)
        {
            _httpClient = client;

        }

        public async Task<string> GetShakespeareTranslation(string text)
        {
            try
            {
                using var content = new StringContent(JsonConvert.SerializeObject(new { text }), Encoding.UTF8, "application/json");
                using var response = await _httpClient.PostAsync("shakespeare.json", content);

                response.EnsureSuccessStatusCode();

                var translationResponse = JsonConvert.DeserializeObject<TranslationResponse>(await response.Content.ReadAsStringAsync());
                return translationResponse.Contents.Translated;
            }
            catch (Exception exception)
            {
                Log.Error($"{MethodBase.GetCurrentMethod()?.DeclaringType} failed with exception {JsonConvert.SerializeObject(exception)}");
                return null;
            }
            
        }

        public async Task<PokemonDetails> GetTranslatedDetails(PokemonDetails pokemon)
        {
            
                var translatedDescription = (pokemon.Habitat == "cave" || pokemon.IsLegendary) ?
                                            await GetYodaTranslation(pokemon.Description) :
                                            await GetShakespeareTranslation(pokemon.Description);

                return new PokemonDetails
                {
                    Id = pokemon.Id,
                    Name = pokemon.Name,
                    Habitat = pokemon.Habitat,
                    Description = translatedDescription ?? pokemon.Description,
                    IsLegendary = pokemon.IsLegendary,
                };
        }

        public async Task<string> GetYodaTranslation(string text)
        {
            try
            {
                using var content = new StringContent(JsonConvert.SerializeObject(new { text }), Encoding.UTF8, "application/json");
                using var response = await _httpClient.PostAsync("yoda.json", content);

                response.EnsureSuccessStatusCode();

                var translationResponse = JsonConvert.DeserializeObject<TranslationResponse>(await response.Content.ReadAsStringAsync());
                return translationResponse.Contents.Translated;
            }
            catch (Exception exception)
            {
                Log.Error($"{MethodBase.GetCurrentMethod()?.DeclaringType} failed with exception {JsonConvert.SerializeObject(exception)}");
                return null;
            }
        }
    }
}
