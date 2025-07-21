using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Interfaces
{
    public interface IArmRepository
    {
        ICollection<Arm> GetArms();
        Arm GetArm(int id);
        bool ArmExists(int id);
        bool CreateArm(Arm arm);

        bool UpdateArm(Arm arm);
        ICollection<Arm> GetPokemonByArm(int pokemonId);

        bool DeleteArm(Arm arm);
        bool Save();
    }
}
