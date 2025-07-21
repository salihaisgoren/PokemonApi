using PokemonReviewAppYenii.Data;
using PokemonReviewAppYenii.Interfaces;
using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Repository
{

    public class ColourRepository : IColourRepository
    {
        private DataContext _context;

        public ColourRepository(DataContext context)
        {
            _context = context;

        }

        public bool CreateColour(Colour colour)
        {
            _context.Add(colour);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;

        }
        public bool ColourExists(int id)
        {
            return _context.Colours.Any(f => f.Id == id);
        }

        public ICollection<Colour> GetColours()
        {

            return _context.Colours.ToList();
        }

        public Colour GetColour(int ColourId)
        {

            return _context.Colours.Where(e => e.Id == ColourId).FirstOrDefault();
        }

        public ICollection<Colour> GetPokemonByColour(int pokemonId)
        {
            return _context.Colours
                           .Where(p => p.PokemonId == pokemonId)
                           .ToList();
        }


        public bool UpdateColour(Colour colour)
        {
            _context.Update(colour);
            return Save();
        }

        public bool DeleteColour(Colour colour)
        {
            _context.Remove(colour);
            return Save();
        }
    }
}
