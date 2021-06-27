using Core.DomainModel;
using DomainServices.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace EFData
{
    public class EFLodgingRepository : EFGenericRepository<Lodging>, ILodgingRepository
    {
        public EFLodgingRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public void Delete(Lodging lodging)
        {
            _dbContext.Lodgings.Remove(lodging);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Animal> GetAnimalsInLodge(int id)
        {
            return GetByID(id).LodgingAnimals;
        }

        public void Update(Lodging lodging)
        {
            Lodging currentLodging = _dbContext.Lodgings.FirstOrDefault(l => l.ID == lodging.ID);
            if (currentLodging != null)
            {
                _dbContext.Entry(currentLodging).CurrentValues.SetValues(lodging);
                _dbContext.SaveChanges();
            }
        }
    }
}