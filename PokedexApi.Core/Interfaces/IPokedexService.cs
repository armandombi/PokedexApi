using PokedexApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexApi.Core.Interfaces
{
    public interface IPokedexService
    {
        Task<PokemonSpecies> GetPokemonSpeciesAsync<T>(T identifier);
    }
}
