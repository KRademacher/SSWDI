using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainServicesHttp.Repositories
{
    public interface IGenericHttpRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetByID(int id);
    }
}