using Core.DomainModel;
using DomainServices.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace EFData
{
    public class EFAnimalRepository : EFGenericRepository<Animal>, IAnimalRepository
    {
        public EFAnimalRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public void Delete(Animal animal)
        {
            _dbContext.Animals.Remove(animal);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Animal> GetAllAvailableAnimals()
        {
            return GetAll().Where(a => a.DateOfPassing == null && a.Adoptable == true).ToList();
        }

        public void Update(Animal animal)
        {
            Animal currentAnimal = _dbContext.Animals.FirstOrDefault(a => a.ID == animal.ID);
            if (currentAnimal != null)
            {
                _dbContext.Entry(currentAnimal).CurrentValues.SetValues(animal);
                _dbContext.SaveChanges();
            }
        }
    }
}