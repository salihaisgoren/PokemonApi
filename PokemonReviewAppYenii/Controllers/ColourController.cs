using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewAppYenii.Dto;
using PokemonReviewAppYenii.Interfaces;
using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Controllers
{
    
        [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
        [ApiController]
        public class ColourController : Controller
        {
            private readonly IColourRepository _colourRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

            public ColourController(IColourRepository colourRepository, IMapper mapper, IPokemonRepository pokemonRepository)
            {
                _colourRepository = colourRepository;
                _mapper = mapper;
                _pokemonRepository = pokemonRepository;

        }

            [HttpGet]
            [ProducesResponseType(200, Type = typeof(IEnumerable<Colour>))]
            public IActionResult GetColours()
            {
                var colours = _mapper.Map<List<ColourDto>>(_colourRepository.GetColours());

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(colours);
            }

            [HttpGet("{colourId}")]
            [ProducesResponseType(200, Type = typeof(Colour))]
            [ProducesResponseType(400)]

            public IActionResult GetColour(int colourId)
            {
                if (!_colourRepository.ColourExists(colourId))
                    return NotFound();

                var colour = _mapper.Map<ColourDto>(_colourRepository.GetColour(colourId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(colour);
            }

            [HttpGet("pokemon/{colourId}")]
            [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
            [ProducesResponseType(400)]

            public IActionResult GetPokemonByColourId(int colourId)
            {
                var pokemons = _mapper.Map<List<PokemonDto>>(_colourRepository.GetPokemonByColour(colourId));

                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(pokemons);
            }

            [HttpPost]
            [ProducesResponseType(204)]
            [ProducesResponseType(400)]
            public IActionResult CreateColour([FromBody] ColourDto colourCreate)
            {
                if (colourCreate == null)
                    return BadRequest(ModelState);

                var colour = _colourRepository.GetColours()
                    .Where(c => c.Name.Trim().ToUpper() == colourCreate.Name.TrimEnd().ToUpper())
                    .FirstOrDefault();

                if (colour != null)
                {
                    ModelState.AddModelError("", "Colour already exists");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                  var pokemonControl = _pokemonRepository.GetPokemons().Any(x => x.Id == colourCreate.PokemonId);

            if (!pokemonControl)
            {
                ModelState.AddModelError("", "Pokemon is not available..");
                return StatusCode(422, ModelState);
            }

            var colourMap = _mapper.Map<Colour>(colourCreate);

                if (!_colourRepository.CreateColour(colourMap))
                {
                    ModelState.AddModelError("", "Something went wrong while savin"); // neden başta çift tırnak.model durumu bir anahtar çiftidir.Bir anahtar ve bir değer ancak burda bir anahtar değeri yoktur.
                    return StatusCode(500, ModelState);
                }

                return Ok("Successfully created");
            }

            [HttpPut("{colourId}")]
            [ProducesResponseType(400)]
            [ProducesResponseType(204)]
            [ProducesResponseType(404)]
            public IActionResult UpdateColour(int colourId, [FromBody] ColourDto updatedColour)
            {
                if (updatedColour == null)
                    return BadRequest(ModelState);

                if (colourId != updatedColour.Id)
                    return BadRequest(ModelState);

                if (!_colourRepository.ColourExists(colourId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest();

            var pokemonControl = _pokemonRepository.GetPokemons().Any(x => x.Id == updatedColour.PokemonId);

            if (!pokemonControl)
            {
                ModelState.AddModelError("", "Böyle bir pokemon mevcut değildir.");
                return StatusCode(422, ModelState);
            }

            var colourMap = _mapper.Map<Colour>(updatedColour);

                if (!_colourRepository.UpdateColour(colourMap))
                {
                    ModelState.AddModelError("", "Something went wrong updating colour ");
                    return StatusCode(500, ModelState);

                }

                return NoContent();

            }

            [HttpDelete("{colourId}")]
            [ProducesResponseType(400)]
            [ProducesResponseType(204)]
            [ProducesResponseType(204)]
            public IActionResult DeleteColour(int colourId)
            {
                if (!_colourRepository.ColourExists(colourId))
                {
                    return NotFound();
                }

                var colourTodelete = _colourRepository.GetColour(colourId);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (!_colourRepository.DeleteColour(colourTodelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting colour ");
                }
                return NoContent();
            }
        }
}
