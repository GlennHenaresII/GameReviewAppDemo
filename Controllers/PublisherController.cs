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
    public class PublisherController : Controller
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IMapper _mapper;

        public PublisherController(IPublisherRepository publisherRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePublisher([FromBody] PublisherDto publisherCreate)
        {
            if (publisherCreate == null)
                return BadRequest(ModelState);

            var publishers = _publisherRepository.GetPublishers()
                .Where(p => p.Name.Trim().ToUpper() == publisherCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (publishers != null)
            {
                ModelState.AddModelError("", "Publisher already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisherMap = _mapper.Map<Publisher>(publisherCreate);

            if (!_publisherRepository.CreatePublisher(publisherMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{publisherId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePublisher(int publisherId, [FromBody] PublisherDto updatedPublisher)
        {
            if (updatedPublisher == null)
                return BadRequest(ModelState);

            if (publisherId != updatedPublisher.Id)
                return BadRequest(ModelState);

            if (!_publisherRepository.PublisherExists(publisherId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var publisherMap = _mapper.Map<Publisher>(updatedPublisher);

            if (!_publisherRepository.UpdatePublisher(publisherMap))
            {
                ModelState.AddModelError("", "Something went wrong updating publisher");
                return StatusCode(500, ModelState);
            }

            return Ok("Publisher updated"); ;
        }

        [HttpDelete("{publisherId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePublisher(int publisherId)
        {
            if (!_publisherRepository.PublisherExists(publisherId))
            {
                return NotFound();
            }

            var publisherToDelete = _publisherRepository.GetPublisher(publisherId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_publisherRepository.DeletePublisher(publisherToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting publisher");
            }

            return Ok("Publisher deleted"); ;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Publisher>))]
        public IActionResult GetPublishers()
        {
            var publishers = _mapper.Map<List<PublisherDto>>(_publisherRepository.GetPublishers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(publishers);
        }

        [HttpGet("{publisherId}")]
        [ProducesResponseType(200, Type = typeof(Publisher))]
        [ProducesResponseType(400)]
        public IActionResult GetPublisher(int publisherId)
        {
            if (!_publisherRepository.PublisherExists(publisherId))
                return NotFound();

            var publisher = _mapper.Map<PublisherDto>(_publisherRepository.GetPublisher(publisherId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(publisher);
        }

        [HttpGet("{publisherId}/games")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
        [ProducesResponseType(400)]
        public IActionResult GetGamesByPublisherId(int publisherId)
        {
            var games = _mapper.Map<List<GameDto>>(_publisherRepository.GetGamesByPublisher(publisherId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(games);
        }
    }
}
