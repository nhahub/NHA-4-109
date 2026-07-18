using DataAccessLayer.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
        public interface IServiceRepository : IGenericRepository<Service>
        {
            IEnumerable<Service> GetByType(string serviceType);
            IEnumerable<Service> GetServicesByProperty(int propertyId);
       }
    
}
