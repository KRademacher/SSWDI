using Core.DomainModel;
using Core.Enums;
using DomainServices.Repositories;
using DomainServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class LodgingService : ILodgingService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ILodgingRepository _lodgingRepository;

        public LodgingService(IAnimalRepository animalRepository, ILodgingRepository lodgingRepository)
        {
            _animalRepository = animalRepository;
            _lodgingRepository = lodgingRepository;
        }

        public void AddAnimalToLodge(Lodging lodging, Animal animal)
        {
            try
            {
                if (lodging.CurrentCapacity + 1 > lodging.MaxCapacity)
                {
                    throw new InvalidOperationException("Chosen lodging is at max capacity.");
                }
                // If animal is already in a lodge
                if (animal.LodgingID != null)
                {
                    RemoveAnimalFromLodge(lodging, animal);
                }

                lodging.LodgingAnimals.Add(animal);
                lodging.CurrentCapacity++;
                animal.LodgingID = lodging.ID;

                _lodgingRepository.Update(lodging);
                _animalRepository.Update(animal);
            }
            catch (InvalidOperationException iOE)
            {
                throw iOE;
            }
        }

        public void Create(Lodging lodging)
        {
            lodging.CurrentCapacity = 0;
            _lodgingRepository.Create(lodging);
        }

        public void Delete(Lodging lodging)
        {
            _lodgingRepository.Delete(lodging);
        }

        public IEnumerable<Lodging> GetAll()
        {
            return _lodgingRepository.GetAll();
        }

        public IEnumerable<Animal> GetAnimalsInLodge(int id)
        {
            return _lodgingRepository.GetAnimalsInLodge(id);
        }

        public Lodging GetByID(int id)
        {
            var lodging = _lodgingRepository.GetByID(id);
            lodging.LodgingAnimals = _animalRepository.GetAll().Where(a => a.LodgingID == lodging.ID).ToList();
            return lodging;
        }

        public IEnumerable<Lodging> GetCompatibleLodgings(int animalId)
        {
            var animal = _animalRepository.GetByID(animalId);
            var lodges = GetAll();
            var compatibleLodges = new List<Lodging>();

            foreach (var lodge in lodges)
            {
                // If the lodge is of the right animal type, and if there is room in the lodge
                if (lodge.AnimalType == animal.AnimalType && lodge.CurrentCapacity + 1 <= lodge.MaxCapacity)
                {
                    // If the lodge is individual
                    if (lodge.LodgingType == LodgingType.Individual)
                    {
                        compatibleLodges.Add(lodge);
                    }
                    else
                    {
                        // If the animal is neutered, it doesn't matter in which group lodge it can stay
                        if (animal.IsNeutered)
                        {
                            compatibleLodges.Add(lodge);
                        }
                        // If it isn't neutered, then it can only stay in group lodges with animals of the same gender
                        else if ((lodge.LodgingType == LodgingType.GroupMale && animal.Gender == Gender.Male) ||
                                (lodge.LodgingType == LodgingType.GroupFemale && animal.Gender == Gender.Female))
                        {
                            compatibleLodges.Add(lodge);
                        }
                    }
                }
            }
            return compatibleLodges;
        }

        public void RemoveAnimalFromLodge(Lodging lodging, Animal animal)
        {
            if (lodging.LodgingAnimals.Contains(animal))
            {
                lodging.LodgingAnimals.Remove(animal);
                lodging.CurrentCapacity--;
                animal.LodgingID = null;

                _lodgingRepository.Update(lodging);
                _animalRepository.Update(animal);
            }
        }

        public void Update(Lodging lodging)
        {
            _lodgingRepository.Update(lodging);
        }
    }
}
