using DataAccessLayer.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Classes;
using DataAccessLayer.ModelContetxt;
namespace BusinessLogicLayer.Repositories
{
    public class TenantRepository : GenericRepository<Tentant>, ITenantRepository
    {
        public TenantRepository(DataAccessLayer.AppContext context) : base(context) { }

        public IEnumerable<Review> GetTenantReviews(int tenantId)
            => _context.Set<Review>()
                       .Where(r => r.Reiewer.UsserId == tenantId)
                       .ToList();

        public IEnumerable<Message> GetTenantMessages(int tenantId)
            => _context.Set<Message>()
                       .Where(m => m.tentant.UsserId == tenantId && !m.IsDeleted)
                       .ToList();
    }
}
