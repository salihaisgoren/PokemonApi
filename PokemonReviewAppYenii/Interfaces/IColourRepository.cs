using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Interfaces
{
    public interface IColourRepository
    {
        ICollection<Colour> GetColours();
        Colour GetColour(int id);
        bool ColourExists(int id);
        bool CreateColour(Colour colour);

        bool UpdateColour(Colour colour);
        ICollection<Colour> GetPokemonByColour(int pokemonId);

        bool DeleteColour(Colour colour);
        bool Save();
    }
}
