using DataAccessLayer.Classes;
using DataAccessLayer.ModelContetxt;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface ITenantRepository : IGenericRepository<Tentant>
    {
        IEnumerable<Review> GetTenantReviews(int tenantId);
        IEnumerable<Message> GetTenantMessages(int tenantId);
    }
}
