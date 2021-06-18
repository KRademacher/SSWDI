using Core.DomainModel;
using DomainService.Repositories;
using System.Collections.Generic;

namespace DomainService.Services
{
    public interface IAnimalService : IAnimalRepository
    {
        public IEnumerable<Animal> GetAllAvailableAnimals();
        public void AddComment(Animal animal, Comment comment);

    }
}