using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DomainServicesHttp.Repositories
{
    public interface IAnimalHttpRepository : IGenericHttpRepository<Animal>
    {
        Task<Animal> Add(Animal animal);

        Task<IEnumerable<Animal>> GetAvailableAnimals();

        Task<IEnumerable<Animal>> GetInterestedAnimals(ClaimsPrincipal user);
    }
}