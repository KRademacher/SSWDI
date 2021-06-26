using Core.DomainModel;
using DomainServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFData
{
    public class EFInterestedAnimalRepository : EFGenericRepository<InterestedAnimal>, IInterestedAnimalRepository
    {
        public EFInterestedAnimalRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public InterestedAnimal Create(Animal animal, Customer customer)
        {
            InterestedAnimal interestedAnimal = new InterestedAnimal()
            {
                AnimalID = animal.ID,
                CustomerID = customer.ID
            };
            _dbContext.InterestedAnimals.Add(interestedAnimal);
            _dbContext.SaveChanges();
            return interestedAnimal;
        }

        public void Delete(Animal animal, Customer customer)
        {
            InterestedAnimal interestedAnimal = new InterestedAnimal()
            {
                AnimalID = animal.ID,
                CustomerID = customer.ID
            };
            _dbContext.InterestedAnimals.Remove(interestedAnimal);
            _dbContext.SaveChanges();
        }

        public InterestedAnimal Get(int customerId, int animalId)
        {
            return _dbContext.InterestedAnimals.First(ia => 
                ia.CustomerID == customerId && ia.AnimalID == animalId);
        }

        public IEnumerable<Animal> GetAllOfCustomer(int customerId)
        {
            var interestedAnimals = _dbContext.InterestedAnimals.Where(c => c.CustomerID == customerId);
            List<Animal> animals = new List<Animal>();
            foreach (var item in interestedAnimals)
            {
                var animal = _dbContext.Animals.FirstOrDefault(a => a.ID == item.AnimalID);
                animals.Add(animal);
            }
            return animals;
        }
    }
}