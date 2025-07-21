using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PokemonReviewAppYenii.Model
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<PokemonOwner> PokemonOwners { get; set; }
        public ICollection<PokemonCategory> PokemonCategories { get; set; }
        public ICollection<PokemonFood> PokemonFoods { get; set; }
        public ICollection<Colour> Colours { get; set; }
        public ICollection<Arm> Arms { get; set; }







    }
}
