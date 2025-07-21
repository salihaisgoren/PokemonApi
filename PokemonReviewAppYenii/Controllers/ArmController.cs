using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewAppYenii.Dto;
using PokemonReviewAppYenii.Interfaces;
using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ArmController : Controller
    {
        private readonly IArmRepository _armRepository;
        private readonly IMapper _mapper;

        public ArmController(IArmRepository armRepository, IMapper mapper)
        {
            _armRepository = armRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Arm>))]
        public IActionResult GetArms()
        {
            var arms = _mapper.Map<List<ArmDto>>(_armRepository.GetArms());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(arms);
        }

        [HttpGet("{armId}")]
        [ProducesResponseType(200, Type = typeof(Arm))]
        [ProducesResponseType(400)]

        public IActionResult GetArm(int armId)
        {
            if (!_armRepository.ArmExists(armId))
                return NotFound();

            var arm = _mapper.Map<ArmDto>(_armRepository.GetArm(armId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(arm);
        }

        [HttpGet("pokemon/{armId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemonByArmId(int armId)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_armRepository.GetPokemonByArm(armId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateArm([FromBody] ArmDto armCreate)
        {
            if (armCreate == null)
                return BadRequest(ModelState);

            var arm = _armRepository.GetArms()
                .Where(c => c.Name.Trim().ToUpper() == armCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (arm != null)
            {
                ModelState.AddModelError("", "Arm already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var armMap = _mapper.Map<Arm>(armCreate);

            if (!_armRepository.CreateArm(armMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin"); 
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{armId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateArm(int armId, [FromBody] ArmDto updatedArm)
        {
            if (updatedArm == null)
                return BadRequest(ModelState);

            if (armId != updatedArm.ArmId)
                return BadRequest(ModelState);

            if (!_armRepository.ArmExists(armId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var armMap = _mapper.Map<Arm>(updatedArm);

            if (!_armRepository.UpdateArm(armMap))
            {
                ModelState.AddModelError("", "Something went wrong updating arm ");
                return StatusCode(500, ModelState);

            }

            return NoContent();

        }

        [HttpDelete("{armId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(204)]
        public IActionResult DeleteArm(int Id)
        {
            if (!_armRepository.ArmExists(Id))
            {
                return NotFound();
            }

            var armTodelete = _armRepository.GetArm(Id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_armRepository.DeleteArm(armTodelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting arm ");
            }
            return NoContent();
        }
    }




}
