using AutoMapper;
using PokemonReviewAppYenii.Dto;
using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>(); // update yaparken dto daha önce.
            CreateMap<Country, CountryDto>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<PokemonDto, Pokemon>();
            CreateMap<ReviewDto, Review>();
            CreateMap<CountryDto, Country>();
            CreateMap<OwnerDto, Owner>();
            CreateMap<ReviewerDto,Reviewer > ();
            CreateMap<Food, FoodDto>();
            CreateMap<FoodDto, Food>();
            CreateMap<ColourDto, Colour>();
            CreateMap<Colour, ColourDto>();
            CreateMap<ArmDto, Arm>();
            CreateMap<Arm, ArmDto>();
            CreateMap<Move, MoveDto>();
            CreateMap<MoveDto, Move>();


        }
    }
}
