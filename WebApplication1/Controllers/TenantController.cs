using Bll.Interfaces;
using DataAccessLayer.Classes;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.DTOs;

namespace PresentationLayer.Controllers
{
    [Route("api/TenantApis")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantRepository _tenantRepository;

        public TenantController(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        [HttpGet("GetAllTenants")]
        public IActionResult GetAllTenants()
        {
            var tenants = _tenantRepository.GetAll()
                .Where(u => !u.IsDeleted)
                .Select(t => new TenantDTO
                {
                    UsserId = t.UsserId,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    PhoneNumber = t.PhoneNumber,
                    Email = t.Email,
                    Password = t.Password
                });

            return Ok(tenants);
        }

        // GET: api/TenantApis/GetTenantById/1
        [HttpGet("GetTenantById/{id}")]
        public IActionResult GetTenantById(int id)
        {
            var tenant = _tenantRepository.GetById(id);

            if (tenant == null)
                return NotFound();

            var dto = new TenantDTO
            {
                UsserId = tenant.UsserId,
                FirstName = tenant.FirstName,
                LastName = tenant.LastName,
                PhoneNumber = tenant.PhoneNumber,
                Email = tenant.Email,
                Password = tenant.Password
            };

            return Ok(dto);
        }

        [HttpGet("GetTenantReviews/{tenantId}")]
        public IActionResult GetTenantReviews(int tenantId)
        {
                    var reviews = _tenantRepository.GetTenantReviews(tenantId)
                .Where(r => !r.IsDeleted)
                .Select(r => new ReviewDTO
                {
                    ReviewID = r.ReviewID,
                    TenantId = r.TenantId,
                    PropertyId = r.PropertyId,
                    Content = r.Content,
                    ReciveDate = r.ReciveDate,
                    ReadDate = r.ReadDate
                })
            .ToList();

            if (!reviews.Any())
                return NotFound("No reviews found.");

            return Ok(reviews);
        }

        [HttpGet("GetTenantMessages/{tenantId}")]
        public IActionResult GetTenantMessages(int tenantId)
        {
            var messages = _tenantRepository.GetTenantMessages(tenantId).Where(m => !m.IsDeleted)
              .Select(m => new SearchMessageDTO
              {
                  MessageID = m.MessageID,
                  Content = m.Content,
                  SenderFlag = m.SenderFlag,
                  ReciveDate = m.ReciveDate,
                  ReadDate = m.ReadDate,
                  OwnerId = m.OwnerUsserId,
                  TentantId = m.tentantUsserId
              })
              .ToList();
            if (!messages.Any())
                return NotFound("No messages found.");

            return Ok(messages);
        }
        [HttpPost("AddTenant")]
        public IActionResult AddTenant([FromBody] TenantDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tenant = new Tentant
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Password = dto.Password
            };

            _tenantRepository.Add(tenant);
            _tenantRepository.Save();

            dto.UsserId = tenant.UsserId;

            return CreatedAtAction(nameof(GetTenantById), new { id = tenant.UsserId }, dto);
        }
        // PUT: api/TenantApis/UpdateTenant/1
        [HttpPut("UpdateTenant/{id}")]
        public IActionResult UpdateTenant(int id, [FromBody] TenantDTO dto)
        {
            if (id != dto.UsserId)
                return BadRequest("Id mismatch.");

            var tenant = _tenantRepository.GetById(id);

            if (tenant == null)
                return NotFound();

            tenant.FirstName = dto.FirstName;
            tenant.LastName = dto.LastName;
            tenant.PhoneNumber = dto.PhoneNumber;
            tenant.Email = dto.Email;
            tenant.Password = dto.Password;

            _tenantRepository.Update(tenant);
            _tenantRepository.Save();

            return Ok(dto);
        }

        [HttpDelete("DeleteTenant/{id}")]
        public IActionResult DeleteTenant(int id)
        {
            var tenant = _tenantRepository.GetById(id);

            if (tenant == null)
                return NotFound();

            _tenantRepository.SoftDelete(tenant);
            _tenantRepository.Save();

            return Ok("Tenant deleted successfully.");
        }

  
        
    }
}