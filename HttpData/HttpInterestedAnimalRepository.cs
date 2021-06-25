using Core.DomainModel;
using DomainServices.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpData
{
    class HttpInterestedAnimalRepository : IInterestedAnimalRepository
    {
        private readonly string apiBaseUrl = "https://localhost:44315";

        public void Create(int animalId, int userId)
        {
            throw new NotImplementedException();
        }

        public void Delete(int animalId, int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InterestedAnimal> GetAll(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
