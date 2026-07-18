using DataAccessLayer.Classes;

using System.Collections.Generic;
using BusinessLogicLayer.Interfaces;


namespace BusinessLogicLayer.Repositories
{
    public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(DataAccessLayer.AppContext context) : base(context) { }
        
        public Owner? GetByBusinessTaxID(string businessTaxID)
            => _dbSet.FirstOrDefault(u => u.BusinessTaxID == businessTaxID && !u.IsDeleted);

        public IEnumerable<Property> GetOwnerProperties(int ownerId)
            => _context.Set<Property>()
                       .Where(p => p.Owner.UsserId == ownerId)
                       .ToList();

        public IEnumerable<Message> GetOwnerMessages(int ownerId)
            => _context.Set<Message>()
                       .Where(m => m.Owner.UsserId == ownerId && !m.IsDeleted)
                       .ToList();
    }
}
