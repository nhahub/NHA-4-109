using DataAccessLayer.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IOwnerRepository : IGenericRepository<Owner>
    {
        Owner? GetByBusinessTaxID(string businessTaxID);
        IEnumerable<Property> GetOwnerProperties(int ownerId);
        IEnumerable<Message> GetOwnerMessages(int ownerId);
    }
}
