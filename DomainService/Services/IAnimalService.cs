using Core.DomainModel;
using DomainServices.Repositories;
using System.Collections.Generic;

namespace DomainServices.Services
{
    public interface IAnimalService : IAnimalRepository
    {
        public IEnumerable<Animal> GetAllAvailableAnimals();
        public void AddComment(Animal animal, Comment comment);

    }
}