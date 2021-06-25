using System.Collections.Generic;

namespace DomainServices.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity);

        IEnumerable<TEntity> GetAll();

        TEntity GetByID(int id);
    }
}