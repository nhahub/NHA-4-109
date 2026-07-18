using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.ModelContetxt;
using DataAccessLayer.Classes;
namespace BusinessLogicLayer.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(DataAccessLayer.AppContext context) : base(context) { }

        public IEnumerable<Service> GetByType(string serviceType)
            => _dbSet.Where(s => s.ServiceType.ToLower() == serviceType.ToLower()).ToList();

        public IEnumerable<Service> GetServicesByProperty(int propertyId)
            => _dbSet.Where(s => s.Properties.Any(p => p.ProperyID == propertyId))
                     .ToList();
    }
}
