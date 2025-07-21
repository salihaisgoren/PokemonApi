using PokemonReviewAppYenii.Data;
using PokemonReviewAppYenii.Interfaces;
using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Repository
{
    public class FoodRepository:IFoodRepository
    {
        private DataContext _context;

        public FoodRepository(DataContext context)
        {
            _context = context;

        }

        public bool CreateFood(Food food)
        {
            _context.Add(food);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;

        }
        public bool FoodExists(int id)
        {
            return _context.Foods.Any(f => f.Id == id);
        }

        public ICollection<Food> GetFoods()
        {
            return _context.Foods.ToList();
        }

        public Food GetFood(int id) // Bir tane ise; Birden fazla ise liste dönderir.
        {
            return _context.Foods.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByFood(int foodid)
        {
            return _context.PokemonFoods.Where(e => e.FoodId == foodid).Select(f => f.Pokemon).ToList();
        }

        public bool UpdateFood(Food food)
        {
            _context.Update(food);
            return Save();
        }

        public bool DeleteFood(Food food)
        {
            _context.Remove(food);
            return Save();



        }
    }
}
