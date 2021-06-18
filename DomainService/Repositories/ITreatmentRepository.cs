using Core.DomainModel;

namespace DomainServices.Repositories
{
    public interface ITreatmentRepository : IGenericRepository<Treatment>
    {
        void Update(Treatment treatment);

        void Delete(Treatment treatment);
    }
}