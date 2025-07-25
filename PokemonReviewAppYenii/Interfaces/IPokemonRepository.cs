﻿using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();

        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPokemonRating(int pokeId);
        bool PokemonExists(int pokeId);
        
        bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon);
        
        bool UpdatePokemon(int ownerId, int categoryId,Pokemon pokemon);
        IEnumerable<PokemonFood> GetPokemonFood(int foodId);

        bool DeletePokemon( Pokemon pokemon);
        bool Save();

    }
}
