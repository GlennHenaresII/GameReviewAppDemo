using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewApp.Dto;
using ReviewApp.Interfaces;
using ReviewApp.Models;
using ReviewApp.Repository;

namespace ReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IMapper _mapper;

        public GameController(IGameRepository gameRepository, IPublisherRepository publisherRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGame([FromQuery] int publisherId, [FromQuery] int[] genreIds, [FromQuery] int[] platformIds, [FromBody] GameDto gameCreate)
        {
            if (gameCreate == null)
                return BadRequest(ModelState);

            var games = _gameRepository.GetGames()
                .Where(c => c.Name.Trim().ToUpper() == gameCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (games != null)
            {
                ModelState.AddModelError("", "Game already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var gameMap = _mapper.Map<Game>(gameCreate);

            gameMap.ReleaseDate = DateTime.Now;
            gameMap.Publisher = _publisherRepository.GetPublisher(publisherId);
            
            if (!_gameRepository.CreateGame(genreIds, platformIds, gameMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{gameId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGame(int gameId, [FromQuery] int[] genreIds, [FromQuery] int[] platformIds, [FromBody] GameUpdateDto updatedGame)
        {
            if (updatedGame == null)
                return BadRequest(ModelState);

            if (gameId != updatedGame.Id)
                return BadRequest(ModelState);

            if (!_gameRepository.GameExists(gameId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var gameMap = _mapper.Map<Game>(updatedGame);

            if (!_gameRepository.UpdateGame(genreIds, platformIds, gameMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return Ok("Game updated");
        }

        [HttpDelete("{gameId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGame(int gameId)
        {
            if (!_gameRepository.GameExists(gameId))
            {
                return NotFound();
            }

            var gameToDelete = _gameRepository.GetGame(gameId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_gameRepository.DeleteGame(gameToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting game");
            }

            return Ok("Game deleted");
        }

        [HttpGet]
        [ProducesResponseType(200, Type= typeof(IEnumerable<Game>))]
        public IActionResult GetGames()
        {
            var games = _mapper.Map<List<GameDto>>(_gameRepository.GetGames());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(games);
        }

        [HttpGet("{gameId}")]
        [ProducesResponseType(200, Type = typeof(Game))]
        [ProducesResponseType(400)]
        public IActionResult GetGame(int gameId)
        {
            if (!_gameRepository.GameExists(gameId))
                return NotFound();

            var game = _mapper.Map<GameDto>(_gameRepository.GetGame(gameId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(game);
        }

        [HttpGet("{gameId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetGameRating(int gameId) 
        {
            if (!_gameRepository.GameExists(gameId)) 
                return NotFound();

            var rating = _gameRepository.GetGameRating(gameId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        [HttpGet("{gameId}/playercount")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPlayerCount(int gameId)
        {
            if (!_gameRepository.GameExists(gameId))
                return NotFound();

            var playercount = _gameRepository.GetPlayerCount(gameId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(playercount);
        }
    }
}
