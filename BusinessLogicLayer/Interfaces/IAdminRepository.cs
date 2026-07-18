using DataAccessLayer.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<Property> GetAllProperties();
    }

}
