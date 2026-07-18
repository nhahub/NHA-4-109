using Bll.Interfaces;
using DataAccessLayer.Classes;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.DTOs;
using PresentationLayer.DTOs.PresentationLayer.DTOs;

namespace PresentationLayer.Controllers
{
    [Route("api/OwnerApis")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnerController(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        // GET: api/OwnerApis/GetAllOwners
        [HttpGet("GetAllOwners")]
        public IActionResult GetAllOwners()
        {
            var owners = _ownerRepository.GetAll()
                .Where(o => !o.IsDeleted)
                .Select(o => new OwnerDTO
                {
                    UsserId = o.UsserId,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    PhoneNumber = o.PhoneNumber,
                    Email = o.Email,
                    Password = o.Password
                });

            return Ok(owners);
        }

        // GET: api/OwnerApis/GetOwnerById/1
        [HttpGet("GetOwnerById/{id}")]
        public IActionResult GetOwnerById(int id)
        {
            var owner = _ownerRepository.GetById(id);

            if (owner == null)
                return NotFound();

            var dto = new OwnerDTO
            {
                UsserId = owner.UsserId,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                PhoneNumber = owner.PhoneNumber,
                Email = owner.Email,
                Password = owner.Password
            };

            return Ok(dto);
        }

        [HttpGet("GetOwnerProperties/{ownerId}")]
        public IActionResult GetOwnerProperties(int ownerId)
        {
            var properties = _ownerRepository.GetOwnerProperties(ownerId);

            if (!properties.Any())
                return NotFound("No properties found.");

            return Ok(properties);
        }

        // GET: api/OwnerApis/GetOwnerMessages/1
        [HttpGet("GetOwnerMessages/{ownerId}")]
        public IActionResult GetOwnerMessages(int ownerId)
        {
            var messages = _ownerRepository.GetOwnerMessages(ownerId);

            if (!messages.Any())
                return NotFound("No messages found.");

            return Ok(messages);
        }

        // POST: api/OwnerApis/AddOwner
        [HttpPost("AddOwner")]
        public IActionResult AddOwner([FromBody] OwnerDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var owner = new Owner
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Password = dto.Password
            };

            _ownerRepository.Add(owner);
            _ownerRepository.Save();

            dto.UsserId = owner.UsserId;

            return CreatedAtAction(nameof(GetOwnerById), new { id = owner.UsserId }, dto);
        }

        // PUT: api/OwnerApis/UpdateOwner/1
        [HttpPut("UpdateOwner")]
        public IActionResult UpdateOwner(int id, [FromBody] OwnerDTO dto)
        {
            if (id != dto.UsserId)
                return BadRequest("Id mismatch.");

            var owner = _ownerRepository.GetById(id);

            if (owner == null)
                return NotFound();

            owner.FirstName = dto.FirstName;
            owner.LastName = dto.LastName;
            owner.PhoneNumber = dto.PhoneNumber;
            owner.Email = dto.Email;
            owner.Password = dto.Password;

            _ownerRepository.Update(owner);
            _ownerRepository.Save();

            return Ok(dto);
        }

     
        [HttpDelete("DeleteOwner/{id}")]
        public IActionResult DeleteOwner(int id)
        {
            var owner = _ownerRepository.GetById(id);

            if (owner == null)
                return NotFound();

            _ownerRepository.SoftDelete(owner);
            _ownerRepository.Save();

            return Ok("Owner deleted successfully.");
        }

        
    }
}