using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainServices.Repositories
{
    public interface IInterestedAnimalRepository
    {
        InterestedAnimal Create(Animal animal, Customer customer);

        void Delete(Animal animal, Customer customer);

        IEnumerable<Animal> GetAll(int customerId);
    }
}