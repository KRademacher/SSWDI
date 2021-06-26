using Core.DomainModel;
using System.Collections.Generic;

namespace DomainServices.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerByID(int id);
        Customer GetCustomerByUserName(string username);
        IEnumerable<Volunteer> GetAllVolunteers();
        Volunteer GetVolunteerByID(int id);
        Volunteer GetVolunteerByUsername(string username);
    }
}