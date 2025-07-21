namespace PokemonReviewAppYenii.Model
{
    public class Colour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        

    }
}
