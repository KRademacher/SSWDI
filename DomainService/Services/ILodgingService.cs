using Core.DomainModel;
using Core.Enums;
using DomainServices.Repositories;
using System.Collections.Generic;

namespace DomainServices.Services
{
    public interface ILodgingService : ILodgingRepository
    {
        void AddAnimalToLodge(Lodging lodging, Animal animal);
        void RemoveAnimalFromLodge(Lodging lodging, Animal animal);
        IEnumerable<Lodging> GetCompatibleLodgings(int animalId);
        IEnumerable<Lodging> GetCompatibleLodgings(AnimalType animalType, Gender gender, bool isNeutered);
    }
}