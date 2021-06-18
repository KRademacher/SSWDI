using Core.DomainModel;

namespace DomainServices.Repositories
{
    public interface IAnimalRepository : IGenericRepository<Animal>
    {
        void Update(Animal animal);

        void Delete(Animal animal);
    }
}