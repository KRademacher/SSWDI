using Core.DomainModel;
using System.Collections.Generic;

namespace DomainServices.Repositories
{
    public interface ITreatmentRepository : IGenericRepository<Treatment>
    {
        void Update(Treatment treatment);

        void Delete(Treatment treatment);

        IEnumerable<Treatment> GetTreatmentsOfAnimal(int animalId);
    }
}