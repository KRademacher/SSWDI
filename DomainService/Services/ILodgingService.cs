using Core.DomainModel;
using DomainServices.Repositories;
using System.Collections.Generic;

namespace DomainServices.Services
{
    public interface ILodgingService : ILodgingRepository
    {
        void AddAnimalToLodge(Lodging lodging, Animal animal);
        void RemoveAnimalFromLodge(Lodging lodging, Animal animal);
        IEnumerable<Animal> GetAnimalsInLodge(int id);
        IEnumerable<Lodging> GetCompatibleLodgings(int animalId);
    }
}