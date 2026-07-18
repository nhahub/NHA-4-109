using Bll.Interfaces;
using DataAccessLayer.Classes;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.DTOs;

namespace PresentationLayer.Controllers
{
    [Route("api/ServiceApis")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IPropertyRepository _propertyRepository;

        public ServiceController(IServiceRepository serviceRepository, IPropertyRepository propertyRepository)
        {
            _serviceRepository = serviceRepository;
            _propertyRepository = propertyRepository;
        }

       
        [HttpGet("getAllServices")]
        public IActionResult GetAll()
        {
            var services = _serviceRepository.GetAll();

            var result = services.Where(s =>s.IsDeleted == false)
                .Select(s => new ServiceDTO
            {
                ServiceID = s.ServiceID,
                ServiceName = s.ServiceName,
                ServiceType = s.ServiceType,
                PhoneNumber = s.PhoneNumber,
                Address = s.Address,
                PropertyIds = s.Properties?.Select(p => p.ProperyID).ToList()
            });

            return Ok(result);
        }

        
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var service = _serviceRepository.GetById(id);

            if (service == null)
                return NotFound();

            var dto = new ServiceDTO
            {
                ServiceID = service.ServiceID,
                ServiceName = service.ServiceName,
                ServiceType = service.ServiceType,
                PhoneNumber = service.PhoneNumber,
                Address = service.Address,
                PropertyIds = service.Properties?.Select(p => p.ProperyID).ToList()
            };

            return Ok(dto);
        }
        [HttpGet("GetServiceByType/{serviceType}")]
        public IActionResult GetByType(string serviceType)
        {
            var services = _serviceRepository.GetByType(serviceType);

            var result = services.Select(s => new ServiceDTO
            {
                ServiceID = s.ServiceID,
                ServiceName = s.ServiceName,
                ServiceType = s.ServiceType,
                PhoneNumber = s.PhoneNumber,
                Address = s.Address,
                PropertyIds = s.Properties?.Select(p => p.ProperyID).ToList()
            });

            return Ok(result);
        }

        
        [HttpGet("GetServicesByProperty/{propertyId}")]
        public IActionResult GetServicesByProperty(int propertyId)
        {
            var services = _serviceRepository.GetServicesByProperty(propertyId);

            var result = services.Select(s => new ServiceDTO
            {
                ServiceID = s.ServiceID,
                ServiceName = s.ServiceName,
                ServiceType = s.ServiceType,
                PhoneNumber = s.PhoneNumber,
                Address = s.Address,
                PropertyIds = s.Properties?.Select(p => p.ProperyID).ToList()
            });

            return Ok(result);
        }



        [HttpPost("AddService")]
        public IActionResult Add(ServiceDTO dto)
        {
            var service = new Service
            {
                ServiceName = dto.ServiceName,
                ServiceType = dto.ServiceType,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address
            };

            foreach (var propertyId in dto.PropertyIds)
            {
                var property = _propertyRepository.GetById(propertyId);
                if (property != null)
                {
                    service.Properties.Add(property);
                }
            }


            _serviceRepository.Add(service);
            _serviceRepository.Save();

            return Ok(service);
        }

        
        [HttpPut("UpdateService/{id}")]
        public IActionResult Update(int id, ServiceDTO dto)
        {
            var service = _serviceRepository.GetById(id);

            if (service == null)
                return NotFound();

            service.ServiceName = dto.ServiceName;
            service.ServiceType = dto.ServiceType;
            service.PhoneNumber = dto.PhoneNumber;
            service.Address = dto.Address;

            foreach (var propertyId in dto.PropertyIds)
            {
                var property = _propertyRepository.GetById(propertyId);
                if (property != null && !service.Properties.Contains(property))
                {
                    service.Properties.Add(property);
                }
            }



            _serviceRepository.Update(service);
            _serviceRepository.Save();

            return Ok(service);
        }

  
        [HttpDelete("DeleteService/{id}")]
        public IActionResult Delete(int id)
        {
            var service = _serviceRepository.GetById(id);

            if (service == null)
                return NotFound();

            _serviceRepository.SoftDelete(service);
            _serviceRepository.Save();

            return Ok("Service deleted successfully.");
        }

        
    }
}