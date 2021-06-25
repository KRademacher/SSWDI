using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainServices.Repositories
{
    public interface IInterestedAnimalRepository
    {
        void Create(int animalId, int userId);

        void Delete(int animalId, int userId);

        IEnumerable<InterestedAnimal> GetAll(int userId);
    }
}