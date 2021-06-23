using Core.DomainModel;
using DomainServices.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Services
{
    public interface IAnimalService : IAnimalRepository
    {
        IEnumerable<Animal> GetAllAvailableAnimals();
        void AddComment(Animal animal, Comment comment);
        void AddTreatment(AnimalTreatment animalTreatment);
        Task<string> UploadImage(Animal animal, string wwwRootPath);
        void RemoveImage(Animal animal, string wwwRootPath);
    }
}