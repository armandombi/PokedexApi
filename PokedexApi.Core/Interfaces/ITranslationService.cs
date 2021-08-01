using PokedexApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexApi.Core.Interfaces
{
    public interface ITranslationService
    {
        Task<string> GetShakespeareTranslation(string text);
        Task<string> GetYodaTranslation(string text);
        Task<PokemonDetails> GetTranslatedDetails(PokemonDetails pokemonDetails);
    }
}
