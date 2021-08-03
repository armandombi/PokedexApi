using Newtonsoft.Json;
using System.Collections.Generic;

namespace PokedexApi.Core.Models
{
    public class PokemonSpecies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("flavor_text_entries")]
        public List<FlavorText> FlavorText { get; set; }
        public NamedApiResource Habitat { get; set; }
        [JsonProperty("is_legendary")]
        public bool IsLegendary { get; set; }
    }
}
