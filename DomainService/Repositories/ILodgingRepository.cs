using Core.DomainModel;
using System.Collections.Generic;

namespace DomainServices.Repositories
{
    public interface ILodgingRepository : IGenericRepository<Lodging>
    {
        IEnumerable<Animal> GetAnimalsInLodge(int id);

        void Update(Lodging lodging);

        void Delete(Lodging lodging);
    }
}
