using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonReviewAppYenii.Data;
using PokemonReviewAppYenii.Dto;
using PokemonReviewAppYenii.Interfaces;
using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class FoodController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;
        

        public FoodController(IFoodRepository foodRepository,IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _foodRepository = foodRepository;
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
            
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Food>))]
        public IActionResult GetFoods()
        {
            var foods = _mapper.Map<List<FoodDto>>(_foodRepository.GetFoods());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(foods);
        }

        [HttpGet("{foodId}")]
        [ProducesResponseType(200, Type = typeof(Food))]
        [ProducesResponseType(400)]

        public IActionResult GetFood(int foodId)
        {
            if (!_foodRepository.FoodExists(foodId))
                return NotFound();

            var food = _mapper.Map<FoodDto>(_foodRepository.GetFood(foodId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(food);
        }

        [HttpGet("pokemon/{foodId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemonByFoodId(int foodId)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_foodRepository.GetPokemonByFood(foodId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(pokemons);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateFood([FromBody] FoodDto foodCreate)
        {
            if (foodCreate == null)
                return BadRequest(ModelState);

            var food = _foodRepository.GetFoods()
                .Where(c => c.Name.Trim().ToUpper() == foodCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (food != null)
            {
                ModelState.AddModelError("", "Food already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonControl = _pokemonRepository.GetPokemons().Any(x => x.Id == foodCreate.PokemonId);

            if(!pokemonControl)
            {
                ModelState.AddModelError("", "Böyle bir pokemon mevcut değildir.");
                return StatusCode(422, ModelState);
            }
            var foodMap = _mapper.Map<Food>(foodCreate);

            if (!_foodRepository.CreateFood(foodMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin"); 
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{foodId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFood(int foodId, [FromBody] FoodDto updatedFood)
        {
            if (updatedFood == null)
                return BadRequest(ModelState);

            if (foodId != updatedFood.Id)
                return BadRequest(ModelState);

            if (!_foodRepository.FoodExists(foodId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var pokemonControl = _pokemonRepository.GetPokemons().Any(x => x.Id == updatedFood.PokemonId);

            if (!pokemonControl)
            {
                ModelState.AddModelError("", "Böyle bir pokemon mevcut değildir.");
                return StatusCode(422, ModelState);
            }

            var foodMap = _mapper.Map<Food>(updatedFood);

            if (!_foodRepository.UpdateFood(foodMap))
            {
                ModelState.AddModelError("", "Something went wrong updating food  ");
                return StatusCode(500, ModelState);

            }

            return NoContent();

        }

        [HttpDelete("{foodId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(204)]
        public IActionResult DeleteFood(int foodId)
        {
            if (!_foodRepository.FoodExists(foodId))
            {
                return NotFound();
            }

            var foodTodelete = _foodRepository.GetFood(foodId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_foodRepository.DeleteFood(foodTodelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting food ");
            }
            return NoContent();
        }

       






    }
}