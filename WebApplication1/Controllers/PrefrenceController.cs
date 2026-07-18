using Bll.Interfaces;
using DataAccessLayer.Classes;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.DTOs;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferencesController : ControllerBase
    {
        private readonly IPreferencesRepository _preferencesRepository;

        public PreferencesController(IPreferencesRepository preferencesRepository)
        {
            _preferencesRepository = preferencesRepository;
        }

    
        [HttpGet("GetAllPreferences")]
        public IActionResult GetAllPreferences()
        {
            var preferences = _preferencesRepository.GetAll();

            var result = preferences
              
                .Select(p => new PreferencesDTO
            {
                PreferencesID = p.PreferencesID,
                SoloOrShared = p.SoloOrShared,
                PreferredLocation = p.PreferredLocation,
                LocationNearness = p.LocationNearness,
                MinPrice = p.MinPrice,
                MaxPrice = p.MaxPrice,
                NumberOfRooms = p.NumberOfRooms,
                Type = p.Type,
                Services = p.Services,
                TenantId = p.TenantId
            });

            return Ok(result);
        }

   
        [HttpGet("GetPreferenceById/{id}")]
        public IActionResult GetPreferenceById(int id)
        {
            var preference = _preferencesRepository.GetById(id);

            if (preference == null)
                return NotFound();

            var dto = new PreferencesDTO
            {
                PreferencesID = preference.PreferencesID,
                SoloOrShared = preference.SoloOrShared,
                PreferredLocation = preference.PreferredLocation,
                LocationNearness = preference.LocationNearness,
                MinPrice = preference.MinPrice,
                MaxPrice = preference.MaxPrice,
                NumberOfRooms = preference.NumberOfRooms,
                Type = preference.Type,
                Services = preference.Services,
                TenantId = preference.TenantId
            };

            return Ok(dto);
        }

        
        [HttpGet("GetPreferenceByTenant/{tenantId}")]
        public IActionResult GetPreferenceByTenant(int tenantId)
        {
            var preference = _preferencesRepository.GetByTenantId(tenantId);

            if (preference == null)
                return NotFound();

            var dto = new PreferencesDTO
            {
                PreferencesID = preference.PreferencesID,
                SoloOrShared = preference.SoloOrShared,
                PreferredLocation = preference.PreferredLocation,
                LocationNearness = preference.LocationNearness,
                MinPrice = preference.MinPrice,
                MaxPrice = preference.MaxPrice,
                NumberOfRooms = preference.NumberOfRooms,
                Type = preference.Type,
                Services = preference.Services,
                TenantId = preference.TenantId
            };

            return Ok(dto);
        }

        
        [HttpGet("GetPreferencesByType/{type}")]
        public IActionResult GetPreferencesByType(string type)
        {
            return Ok(_preferencesRepository.GetByType(type));
        }

        // GET: api/Preferences/GetPreferencesByPriceRange?minPrice=1000&maxPrice=5000
        [HttpGet("GetPreferencesByPriceRange")]
        public IActionResult GetPreferencesByPriceRange(int minPrice, int maxPrice)
        {
            return Ok(_preferencesRepository.GetByPriceRange(minPrice, maxPrice));
        }

        [HttpGet("GetPreferencesBySoloOrShared/{value}")]
        public IActionResult GetPreferencesBySoloOrShared(string value)
        {


            return Ok(_preferencesRepository.GetBySoloOrShared(value));
        }

        
        [HttpPost("AddPreference")]
        public IActionResult AddPreference([FromBody] PreferencesDTO dto)
        {
            var preference = new Preferences
            {
                SoloOrShared = dto.SoloOrShared,
                PreferredLocation = dto.PreferredLocation,
                LocationNearness = dto.LocationNearness,
                MinPrice = dto.MinPrice,
                MaxPrice = dto.MaxPrice,
                NumberOfRooms = dto.NumberOfRooms,
                Type = dto.Type,
                Services = dto.Services,
                TenantId = dto.TenantId
            };

            _preferencesRepository.Add(preference);
            _preferencesRepository.Save();

            return CreatedAtAction(
                nameof(GetPreferenceById),
                new { id = preference.PreferencesID },
                preference);
        }

        
        [HttpPut("UpdatePreference/{id}")]
        public IActionResult UpdatePreference(int id, [FromBody] PreferencesDTO dto)
        {
            var preference = _preferencesRepository.GetById(id);

            if (preference == null)
                return NotFound();

            preference.SoloOrShared = dto.SoloOrShared;
            preference.PreferredLocation = dto.PreferredLocation;
            preference.LocationNearness = dto.LocationNearness;
            preference.MinPrice = dto.MinPrice;
            preference.MaxPrice = dto.MaxPrice;
            preference.NumberOfRooms = dto.NumberOfRooms;
            preference.Type = dto.Type;
            preference.Services = dto.Services;
            preference.TenantId = dto.TenantId;

            _preferencesRepository.Update(preference);
            _preferencesRepository.Save();

            return Ok(preference);
        }

        // DELETE: api/Preferences/DeletePreference/1
        [HttpDelete("DeletePreference/{id}")]
        public IActionResult DeletePreference(int id)
        {
            var preference = _preferencesRepository.GetById(id);

            if (preference == null)
                return NotFound();

            _preferencesRepository.SoftDelete(preference);
            _preferencesRepository.Save();

            return Ok("Preference deleted successfully.");
        }
    }
}