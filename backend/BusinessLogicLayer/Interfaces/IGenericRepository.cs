using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);


        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);

        void Save();
    }
}
