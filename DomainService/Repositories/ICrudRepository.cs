using System.Collections.Generic;

namespace DomainService.Repositories
{
    public interface ICrudRepository<TEntity> where TEntity :  class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetByID(int id);

        void Create(TEntity entity);

        void Delete(TEntity entity);
    }
}