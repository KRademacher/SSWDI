using Core.DomainModel;
using System.Collections.Generic;

namespace DomainServices.Repositories
{
    public interface IAnimalRepository : IGenericRepository<Animal>
    {
        IEnumerable<Animal> GetAllAvailableAnimals();

        void Update(Animal animal);

        void Delete(Animal animal);
    }
}