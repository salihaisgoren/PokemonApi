using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewAppYenii.Dto;
using PokemonReviewAppYenii.Interfaces;
using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Controllers
{

    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class MoveController : Controller
    {
        private readonly IMoveRepository _moveRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public MoveController(IMoveRepository moveRepository, IMapper mapper, IPokemonRepository pokemonRepository)
        {
            _moveRepository = moveRepository;
            _mapper = mapper;
            _pokemonRepository = pokemonRepository;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Move>))]
        public IActionResult GetMoves()
        {
            var moves = _mapper.Map<List<MoveDto>>(_moveRepository.GetMoves());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(moves);
        }

        [HttpGet("{moveId}")]
        [ProducesResponseType(200, Type = typeof(Move))]
        [ProducesResponseType(400)]

        public IActionResult GetMove(int moveId)
        {
            if (!_moveRepository.MoveExists(moveId))
                return NotFound();

            var move = _mapper.Map<MoveDto>(_moveRepository.GetMove(moveId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(move);
        }

        [HttpGet("pokemon/{moveId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemonByColourId(int moveId)
        {
            var moves = _mapper.Map<List<PokemonDto>>(_moveRepository.GetPokemonByMove(moveId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(moves);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMove([FromBody] MoveDto moveCreate)
        {
            if (moveCreate == null)
                return BadRequest(ModelState);

            var move = _moveRepository.GetMoves()
                .Where(c => c.Name.Trim().ToUpper() == moveCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (move != null)
            {
                ModelState.AddModelError("", "Move already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonControl = _pokemonRepository.GetPokemons().Any(x => x.Id == moveCreate.PokemonId);

            if (!pokemonControl)
            {
                ModelState.AddModelError("", "Pokemon is not available..");
                return StatusCode(422, ModelState);
            }

            var moveMap = _mapper.Map<Move>(moveCreate);

            if (!_moveRepository.CreateMove(moveMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin"); // neden başta çift tırnak.model durumu bir anahtar çiftidir.Bir anahtar ve bir değer ancak burda bir anahtar değeri yoktur.
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{moveId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMove(int moveId, [FromBody] MoveDto updatedMove)
        {
            if (updatedMove == null)
                return BadRequest(ModelState);

            if (moveId != updatedMove.Id)
                return BadRequest(ModelState);

            if (!_moveRepository.MoveExists(moveId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var pokemonControl = _pokemonRepository.GetPokemons().Any(x => x.Id == updatedMove.PokemonId);

            if (!pokemonControl)
            {
                ModelState.AddModelError("", "Böyle bir pokemon mevcut değildir.");
                return StatusCode(422, ModelState);
            }

            var moveMap = _mapper.Map<Move>(updatedMove);

            if (!_moveRepository.UpdateMove(moveMap))
            {
                ModelState.AddModelError("", "Something went wrong updating move ");
                return StatusCode(500, ModelState);

            }

            return NoContent();

        }

        [HttpDelete("{moveId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(204)]
        public IActionResult DeleteMove(int moveId)
        {
            if (!_moveRepository.MoveExists(moveId))
            {
                return NotFound();
            }

            var moveTodelete = _moveRepository.GetMove(moveId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_moveRepository.DeleteMove(moveTodelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting move ");
            }
            return NoContent();
        }
    }
}

