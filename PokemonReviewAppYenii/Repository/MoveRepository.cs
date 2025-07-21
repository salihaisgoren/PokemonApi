using PokemonReviewAppYenii.Data;
using PokemonReviewAppYenii.Interfaces;
using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Repository
{
    public class MoveRepository : IMoveRepository
    {
        private DataContext _context;

        public MoveRepository(DataContext context)
        {
            _context = context;

        }

        public bool CreateMove(Move move)
        {
            _context.Add(move);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;

        }
        public bool MoveExists(int id)
        {
            return _context.Moves.Any(f => f.Id == id);
        }

        public ICollection<Move> GetMoves()
        {
            return _context.Moves.ToList();
        }

        public Move GetMove(int id)
        {
            return _context.Moves.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Move> GetPokemonByMove(int pokemonId)
        {
            return _context.Moves.Where(e => e.PokemonId == pokemonId).ToList();
        }

        public bool UpdateMove(Move move)
        {
            _context.Update(move);
            return Save();
        }

        public bool DeleteMove(Move move)
        {
            _context.Remove(move);
            return Save();
        }
    }
}
