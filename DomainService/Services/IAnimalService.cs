using Core.DomainModel;
using DomainService.Repositories;
using System.Collections.Generic;

namespace DomainService.Services
{
    public interface IAnimalService : IAnimalRepository
    {
        public IEnumerable<Animal> GetAllAvailableAnimals();


    }
}