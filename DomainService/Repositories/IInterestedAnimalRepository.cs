using Core.DomainModel;
using System.Collections.Generic;

namespace DomainServices.Repositories
{
    public interface IInterestedAnimalRepository
    {
        InterestedAnimal Create(Animal animal, Customer customer);

        void Delete(Animal animal, Customer customer);

        InterestedAnimal Get(int customerId, int animalId);

        IEnumerable<Animal> GetAll(int customerId);
    }
}