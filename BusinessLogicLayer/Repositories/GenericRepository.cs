using System.Collections.Generic;
using System.Reflection;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer;        
using DataAccessLayer.ModelContetxt;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DataAccessLayer.AppContext _context;
        protected readonly DbSet<T> _dbSet;
        private readonly DataAccessLayer.AppContext context;

        public GenericRepository(DataAccessLayer.AppContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll() => _dbSet.ToList();

        public T? GetById(int id) => _dbSet.Find(id);

        public void Add(T entity) => _dbSet.Add(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public void Save()
        {
            _context.SaveChanges();
        }

        public void SoftDelete(T entity)
        {
            PropertyInfo? prop = typeof(T).GetProperty("IsDeleted");
            if (prop != null && prop.PropertyType == typeof(bool))
            {
                prop.SetValue(entity, true);
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }
        }
    }
}
