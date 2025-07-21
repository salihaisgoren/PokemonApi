using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Interfaces
{
    public interface IMoveRepository
    {
        ICollection<Move> GetMoves();
        Move GetMove(int id);
        bool MoveExists(int id);
        bool CreateMove(Move move);

        bool UpdateMove(Move move);
      
        ICollection<Move> GetPokemonByMove(int pokemonId);

        bool DeleteMove(Move move);
        bool Save();
    }
}
