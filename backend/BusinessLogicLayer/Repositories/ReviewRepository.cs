using DataAccessLayer.ModelContetxt;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(DataAccessLayer.AppContext context) : base(context) { }

        public IEnumerable<Review> GetReviewsByProperty(int propertyId)
            => _dbSet.Where(r => r.ReviewdProperty.ProperyID == propertyId).ToList();

        public IEnumerable<Review> GetReviewsByTenant(int tenantId)
            => _dbSet.Where(r => r.Reiewer.UsserId == tenantId).ToList();
    }
}
