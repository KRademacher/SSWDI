using Core.DomainModel;

namespace DomainServices.Repositories
{
    public interface ILodgingRepository : IGenericRepository<Lodging>
    {
        void Update(Lodging lodging);

        void Delete(Lodging lodging);
    }
}
