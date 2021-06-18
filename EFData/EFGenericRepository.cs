using DomainServices.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace EFData
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public EFGenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public TEntity GetByID(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
    }
}