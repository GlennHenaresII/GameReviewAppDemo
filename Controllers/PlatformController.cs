using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewApp.Dto;
using ReviewApp.Interfaces;
using ReviewApp.Models;

namespace ReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : Controller
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;

        public PlatformController(IPlatformRepository platformRepository, IMapper mapper)
        {
            _platformRepository = platformRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePlatform([FromBody] PlatformDto platformCreate)
        {
            if (platformCreate == null)
                return BadRequest(ModelState);

            var platforms = _platformRepository.GetPlatforms()
                .Where(p => p.Name.Trim().ToUpper() == platformCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (platforms != null)
            {
                ModelState.AddModelError("", "Platform already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var platformMap = _mapper.Map<Platform>(platformCreate);

            if (!_platformRepository.CreatePlatform(platformMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{platformId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePlatform(int platformId, [FromBody] PlatformDto updatedPlatform)
        {
            if (updatedPlatform == null)
                return BadRequest(ModelState);

            if (platformId != updatedPlatform.Id)
                return BadRequest(ModelState);

            if (!_platformRepository.PlatformExists(platformId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var platformMap = _mapper.Map<Platform>(updatedPlatform);

            if (!_platformRepository.UpdatePlatform(platformMap))
            {
                ModelState.AddModelError("", "Something went wrong updating platform");
                return StatusCode(500, ModelState);
            }

            return Ok("Platform updated");
        }

        [HttpDelete("{platformId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePlatform(int platformId)
        {
            if (!_platformRepository.PlatformExists(platformId))
            {
                return NotFound();
            }

            var platformToDelete = _platformRepository.GetPlatform(platformId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_platformRepository.DeletePlatform(platformToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting platform");
            }

            return Ok("Platform deleted");
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Platform>))]
        public IActionResult GetPlatforms()
        {
            var platforms = _mapper.Map<List<PlatformDto>>(_platformRepository.GetPlatforms());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(platforms);
        }

        [HttpGet("{platformId}")]
        [ProducesResponseType(200, Type = typeof(Platform))]
        [ProducesResponseType(400)]
        public IActionResult GetPlatform(int platformId)
        {
            if (!_platformRepository.PlatformExists(platformId))
                return NotFound();

            var platform = _mapper.Map<PlatformDto>(_platformRepository.GetPlatform(platformId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(platform);
        }

        [HttpGet("{platformId}/games")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
        [ProducesResponseType(400)]
        public IActionResult GetGameByPlatform(int platformId)
        {
            var games = _mapper.Map<List<GameDto>>(_platformRepository.GetGamesByPlatform(platformId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(games);
        }
    }
}
