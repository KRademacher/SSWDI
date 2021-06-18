using Core.DomainModel;

namespace DomainService.Repositories
{
    public interface IAnimalRepository : ICrudRepository<Animal>
    {
        void SaveAnimal(Animal animal);
    }
}