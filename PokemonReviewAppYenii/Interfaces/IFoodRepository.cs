using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Interfaces
{
    public interface IFoodRepository
    {
        ICollection<Food> GetFoods();
        Food GetFood(int id);
        ICollection<Pokemon> GetPokemonByFood(int foodid);
        bool FoodExists(int id);
        bool CreateFood(Food food);

        bool UpdateFood(Food food);
        

        bool DeleteFood(Food food);
        bool Save();
    }
}
