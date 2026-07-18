using System.IdentityModel.Tokens.Jwt;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.DTOs;

namespace PresentationLayer.Controllers
{
    [Route("api/PropertyApis")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IAuthorizationService _authorizationService;

        public PropertyController(
            IPropertyRepository propertyRepository, IServiceRepository serviceRepository, IAuthorizationService authorizationService)
        {
            _propertyRepository = propertyRepository;
            _serviceRepository= serviceRepository;
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        [HttpGet("GetAllProperties")]
        public IActionResult GetAllProperties()
        {
            var properties = _propertyRepository.GetAll().Where(p => !p.IsDeleted)
                .Select(p => new PropertyDTO
                {
                    ProperyID = p.ProperyID,
                    NumberOFRooms = p.NumberOFRooms,
                    RentPrice = p.RentPrice,
                    Description = p.Description,
                    ImageUrls = p.ImageUrls,
                    OwnerId = p.OwnerId,
                    AdminId = p.ManageAdminId,
                });

            return Ok(properties);
        }

        [AllowAnonymous]
        [HttpGet("GetPropertyById/{id}")]
        public IActionResult GetPropertyById(int id)
        {
            var property = _propertyRepository.GetById(id);

            if (property == null || property.IsDeleted)
                return NotFound();

            var dto = new PropertyDTO
            {
                ProperyID = property.ProperyID,
                NumberOFRooms = property.NumberOFRooms,
                RentPrice = property.RentPrice,
                Description = property.Description,
                ImageUrls = property.ImageUrls,
                OwnerId = property.OwnerId,
                AdminId = property.ManageAdminId
            };

            return Ok(dto);
        }

        [AllowAnonymous]
        [HttpGet("GetByPriceRange")]
        public IActionResult GetByPriceRange(int minPrice, int maxPrice)
        {
            var properties = _propertyRepository.GetByPriceRange(minPrice, maxPrice).Where(p => !p.IsDeleted)
                .Select(p => new PropertyDTO
                {
                    ProperyID = p.ProperyID,
                    NumberOFRooms = p.NumberOFRooms,
                    RentPrice = p.RentPrice,
                    Description = p.Description,
                    ImageUrls = p.ImageUrls,
                    OwnerId = p.OwnerId,
                    AdminId = p.ManageAdminId
                });

            return Ok(properties);
        }

        [AllowAnonymous]       
        [HttpGet("GetByRooms/{rooms}")]
        public IActionResult GetByRooms(int rooms)
        {
            var properties = _propertyRepository.GetByRooms(rooms).Where(p => !p.IsDeleted)
                .Select(p => new PropertyDTO
                {
                    ProperyID = p.ProperyID,
                    NumberOFRooms = p.NumberOFRooms,
                    RentPrice = p.RentPrice,
                    Description = p.Description,
                    ImageUrls = p.ImageUrls,
                    OwnerId = p.OwnerId,
                    AdminId = p.ManageAdminId
                });

            return Ok(properties);
        }
        
        [AllowAnonymous]
        [HttpGet("GetPropertyReviews/{propertyId}")]
        public IActionResult GetPropertyReviews(int propertyId)
        {
            var reviews = _propertyRepository.GetPropertyReviews(propertyId).Where(p => !p.IsDeleted);

            if (!reviews.Any())
                return NotFound("No reviews found.");

            return Ok(reviews);
        }

        [AllowAnonymous]
        [HttpGet("GetPropertyServices/{propertyId}")]
        public IActionResult GetPropertyServices(int propertyId)
        {
            var services = _propertyRepository.GetPropertyServices(propertyId).Where(p => !p.IsDeleted);

            if (!services.Any())
                return NotFound("No services found.");

            return Ok(services);
        }

        [Authorize(Roles = "Admin,Owner")]
        [HttpPost("AddProperty")]
        public IActionResult AddProperty([FromBody] PropertyDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var property = new Property
            {
                NumberOFRooms = dto.NumberOFRooms,
                RentPrice = dto.RentPrice,
                Description = dto.Description,
                ImageUrls = dto.ImageUrls,
                // modified for IOwned
                OwnerId = User.IsInRole("Owner")
                    ? int.Parse(User.FindFirst(JwtRegisteredClaimNames.Sub)!.Value)
                    : dto.OwnerId,
                ManageAdminId = dto.AdminId,
                Services = new List<Service>()
            };

            foreach (int serviceId in dto.servicesID) 
            {
            
                var service = _serviceRepository.GetById(serviceId);

                if (service != null)
                {
                    property.Services.Add(service);
                }
            }
            
            _propertyRepository.Add(property);
            _propertyRepository.Save();

            dto.ProperyID = property.ProperyID;

            return CreatedAtAction(nameof(GetPropertyById),
                new { id = property.ProperyID }, dto);
        }

        [Authorize(Roles = "Admin,Owner")]
        [HttpPut("UpdateProperty/{id}")]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody] PropertyDTO dto)
        {
            if (id != dto.ProperyID)
                return BadRequest("Id mismatch.");

            var property = _propertyRepository.GetById(id);

            if (property == null)
                return NotFound();
            
            // added for IOwned
            if (User.IsInRole("Owner"))
            {
                var authResult = await _authorizationService.AuthorizeAsync(User, property, "MustOwnProperty");
                if (!authResult.Succeeded)
                    return Forbid();
            }

            property.NumberOFRooms = dto.NumberOFRooms;
            property.RentPrice = dto.RentPrice;
            property.Description = dto.Description;
            property.ImageUrls = dto.ImageUrls;
            property.OwnerId = dto.OwnerId;
            property.ManageAdminId = dto.AdminId;

            foreach (int serviceId in dto.servicesID)
            {

                var service = _serviceRepository.GetById(serviceId);

                if (service != null)
                {
                    property.Services.Add(service);
                }
            }
            
            _propertyRepository.Update(property);
            _propertyRepository.Save();

            return Ok(dto);
        }
        
        [Authorize(Roles = "Admin,Owner")]
        [HttpDelete("DeleteProperty/{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var property = _propertyRepository.GetById(id);

            if (property == null)
                return NotFound();
            
            // added for IOwned
            if (User.IsInRole("Owner"))
            {
                var authResult = await _authorizationService.AuthorizeAsync(User, property, "MustOwnProperty");
                if (!authResult.Succeeded)
                    return Forbid();
            }

            _propertyRepository.SoftDelete(property);
            _propertyRepository.Save();

            return Ok("Property deleted successfully.");
        }

      
    }
}