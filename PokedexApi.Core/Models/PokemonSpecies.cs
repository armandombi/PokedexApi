using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
