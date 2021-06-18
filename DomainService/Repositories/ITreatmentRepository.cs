using Core.DomainModel;

namespace DomainService.Repositories
{
    public interface ITreatmentRepository : ICrudRepository<Treatment>
    {
        void SaveTreatment(Treatment treatment);
    }
}