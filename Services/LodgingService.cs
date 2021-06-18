using Core.DomainModel;
using Core.Enums;
using DomainServices.Repositories;
using DomainServices.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    class LodgingService : ILodgingService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ILodgingRepository _lodgingRepository;

        public void Create(Lodging lodging)
        {
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

        public Lodging GetByID(int id)
        {
            return _lodgingRepository.GetByID(id);
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

        public void Update(Lodging lodging)
        {
            _lodgingRepository.Update(lodging);
        }
    }
}
