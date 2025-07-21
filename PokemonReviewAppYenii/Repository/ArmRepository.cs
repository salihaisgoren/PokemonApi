using PokemonReviewAppYenii.Data;
using PokemonReviewAppYenii.Interfaces;
using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Repository
{
    public class ArmRepository:IArmRepository
    {
        private DataContext _context;

        public ArmRepository(DataContext context)
        {
            _context = context;

        }

        public bool CreateArm(Arm arm)
        {
            _context.Add(arm);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;

        }
        public bool ArmExists(int id)
        {
            return _context.Arms.Any(f => f.Id == id);
        }

        public ICollection<Arm> GetArms()
        {

            return _context.Arms.ToList();
        }

        public Arm GetArm(int ArmId)
        {

            return _context.Arms.Where(e => e.Id == ArmId).FirstOrDefault();
        }

        public ICollection<Arm> GetPokemonByArm(int pokemonId)
        {
            return _context.Arms
                           .Where(p => p.PokemonId == pokemonId)
                           .ToList();
        }


        public bool UpdateArm(Arm arm)
        {
            _context.Update(arm);
            return Save();
        }

        public bool DeleteArm(Arm arm)
        {
            _context.Remove(arm);
            return Save();
        }
    }
}
