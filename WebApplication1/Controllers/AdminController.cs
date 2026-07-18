using Bll.Interfaces;
using DataAccessLayer.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.DTOs;

namespace PresentationLayer.Controllers
{
    [Route("api/AdminApis")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
         [HttpGet("properties")]
        public IActionResult GetAllProperties()
        {
            var properties = _adminRepository.GetAllProperties();

            if (properties == null || !properties.Any())
                return NotFound("No properties found.");

            return Ok(properties);
        }

        [HttpGet("GetAllAdmins")]
        public IActionResult GetAllAdmins()
        {
            var admins = _adminRepository.GetAll().Where(u => !u.IsDeleted)
                .Select(a => new AdminDTO
                {
                    AdminId=a.UsserId,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    PhoneNumber = a.PhoneNumber,
                    Email = a.Email,
                    Password = a.Password
                })
                .ToList();

            return Ok(admins);
        }

        [HttpGet("GetAdmin{id}")]
        public IActionResult GetAdminById(int id)
        {
            var admin = _adminRepository.GetById(id);

            if (admin == null)
                return NotFound();

            var dto = new AdminDTO
            {
                AdminId = admin.UsserId,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                PhoneNumber = admin.PhoneNumber,
                Email = admin.Email,
                Password = admin.Password
            };

            return Ok(dto);
        }
        [HttpPost("AddAdmin")]
        public IActionResult AddAdmin(AdminDTO dto)
        {
            Admin admin = new Admin
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Password = dto.Password
            };

            _adminRepository.Add(admin);
            _adminRepository.Save();

            AdminDTO response = new AdminDTO
            {
                AdminId = admin.UsserId,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                PhoneNumber = admin.PhoneNumber,
                Email = admin.Email,
                Password = admin.Password
            };

            return Ok(response);
        }

        [HttpPut("UpdateAdmin{id}")]
        public IActionResult UpdateAdmin(int id, AdminDTO dto)
        {
            var admin = _adminRepository.GetById(id);

            if (admin == null)
                return NotFound();

            admin.FirstName = dto.FirstName;
            admin.LastName = dto.LastName;
            admin.PhoneNumber = dto.PhoneNumber;
            admin.Email = dto.Email;
            admin.Password = dto.Password;

            _adminRepository.Update(admin);
            _adminRepository.Save();

            return Ok("update successful");
        }

        [HttpDelete("DeleteAdmin/{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            var admin = _adminRepository.GetById(id);

            if (admin == null)
                return NotFound();

            _adminRepository.SoftDelete(admin);
            _adminRepository.Save();

            return Ok("delete successful");
        }

    }
}
