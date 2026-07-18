using System.Collections.Generic;
using System.Reflection;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.ModelContetxt;
using DataAccessLayer.Classes;



namespace BusinessLogicLayer.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        private readonly DataAccessLayer.AppContext context;

        public AdminRepository(DataAccessLayer.AppContext context) : base(context)
        {
            this.context = context;
          
        }
        public IEnumerable<User> GetAllUsers()
            => _context.Set<User>().Where(u => !u.IsDeleted).ToList();

        public IEnumerable<Property> GetAllProperties()
            => _context.Set<Property>().ToList();

        public override bool Equals(object? obj)
        {
            return obj is AdminRepository repository &&
                   EqualityComparer<DataAccessLayer.AppContext>.Default.Equals(context, repository.context);
        }
    }
}
