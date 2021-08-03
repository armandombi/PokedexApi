using PokedexApi.Core.Models;
using System.Threading.Tasks;

namespace PokedexApi.Core.Interfaces
{
    public interface IPokedexService
    {
        Task<PokemonSpecies> GetPokemonSpecies<T>(T identifier);
    }
}
