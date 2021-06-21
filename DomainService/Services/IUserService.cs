using Core.DomainModel;
using DomainServices.Repositories;
using Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DomainServices.Services
{
    public interface IUserService : IUserRepository
    {
        Task<IdentityResult> RegisterCustomer(Customer customer, ApplicationUser applicationUser);
        Task<IdentityResult> RegisterVolunteer(Volunteer volunteer, ApplicationUser applicationUser);
    }
}