namespace PokedexApi.Core.Models
{
    public class PokemonDetails
    {
        public PokemonDetails()
        {

        }

        public PokemonDetails(PokemonSpecies species)
        {
            Id = species.Id;
            Name = species.Name;
            Description = species.FlavorText.Find(ft => ft.Language.Name == "en").Text;
            Habitat = species.Habitat.Name;
            IsLegendary = species.IsLegendary;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Habitat { get; set; }
        public bool IsLegendary { get; set; }
    }
}
