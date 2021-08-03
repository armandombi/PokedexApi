using Newtonsoft.Json;

namespace PokedexApi.Core.Models
{
    public class FlavorText
    {
        [JsonProperty("flavor_text")]
        public string Text { get; set; }
        public NamedApiResource Language { get; set; }
    }
}
