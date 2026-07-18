using DataAccessLayer.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User? GetByEmail(string email);
        User? GetByPhone(string phoneNumber);
        User? GetByNationalID(string nationalID);
        bool CheckPassword(string email, string password);
    }
}
