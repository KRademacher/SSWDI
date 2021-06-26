using Core.DomainModel;
using System.Collections.Generic;

namespace DomainServices.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer FindCustomerByID(int id);
        Customer FindCustomerByUserName(string username);
        IEnumerable<Volunteer> GetAllVolunteers();
        Volunteer FindVolunteerByID(int id);
        Volunteer FindVolunteerByUsername(string username);
    }
}