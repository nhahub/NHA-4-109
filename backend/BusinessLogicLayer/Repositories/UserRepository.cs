using DataAccessLayer.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataAccessLayer.AppContext context) : base(context) { }

        public User? GetByEmail(string email)
            => _dbSet.FirstOrDefault(u => u.Email == email && !u.IsDeleted);

        public User? GetByPhone(string phoneNumber)
            => _dbSet.FirstOrDefault(u => u.PhoneNumber == phoneNumber && !u.IsDeleted);
        
        public User? GetByNationalID(string nationalID)
            => _dbSet.FirstOrDefault(u => u.NationalID == nationalID && !u.IsDeleted);

        public bool CheckPassword(string email, string password)
        {
            var user = _dbSet.SingleOrDefault(u => u.Email == email && !u.IsDeleted);
            return user is not null && BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}
