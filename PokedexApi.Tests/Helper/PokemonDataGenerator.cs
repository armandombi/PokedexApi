using PokedexApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexApi.Tests.Helper
{
    internal static class PokemonDataGenerator
    {
        internal static PokemonSpecies GenerateValidPokemonSpecies(string name, string habitat, bool isLegendary)
        {
            return new PokemonSpecies
            {
                Id = 1,
                Name = name,
                FlavorText = new List<FlavorText>
                {
                    new FlavorText{ Language = new NamedApiResource { Name = "en", Url = "http://loremipsumen.com" }, Text = "When several of\nthese POKéMON\ngather, their\felectricity could\nbuild and cause\nlightning storms."},
                    new FlavorText{ Language = new NamedApiResource { Name = "fr", Url = "http://loremipsumfr.com" }, Text = "Il lui arrive de remettre d’aplomb\nun Pikachu allié en lui envoyant\nune décharge électrique."},
                    new FlavorText{ Language = new NamedApiResource { Name = "de", Url = "http://loremipsum.com" }, Text = "Es streckt seinen Schweif nach oben, um seine\nUmgebung zu prüfen. Häufig fährt ein Blitz hinein."}
                },
                Habitat = new NamedApiResource
                {
                    Name = habitat
                },
                IsLegendary = isLegendary
            };
        }
    }
}
