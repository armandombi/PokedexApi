using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexApi.Core.Models
{
    public class FlavorText
    {
        [JsonProperty("flavor_text")]
        public string Text { get; set; }
        public NamedApiResource Language { get; set; }
    }
}
