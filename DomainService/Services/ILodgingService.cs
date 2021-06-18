using Core.DomainModel;
using DomainServices.Repositories;
using System.Collections.Generic;

namespace DomainServices.Services
{
    public interface ILodgingService : ILodgingRepository
    {
        IEnumerable<Lodging> GetCompatibleLodgings(int animalId);
    }
}