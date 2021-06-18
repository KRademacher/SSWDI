using System.Collections.Generic;

namespace DomainServices.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);

        IEnumerable<TEntity> GetAll();

        TEntity GetByID(int id);
    }
}