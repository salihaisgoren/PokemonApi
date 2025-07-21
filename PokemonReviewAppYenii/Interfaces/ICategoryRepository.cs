using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Pokemon> GetPokemonByCategory(int categoryid);
        bool categoryExists(int id);
        bool CreateCategory(Category category);
       
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();


    }
}