using Core.DomainModel;
using DomainServices.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace EFData
{
    public class EFUserRepository : EFGenericRepository<User>, IUserRepository
    {
        public EFUserRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public Customer GetCustomerByID(int id)
        {
            return _dbContext.Customers.FirstOrDefault(u => u.ID == id);
        }

        public Customer GetCustomerByUserName(string username)
        {
            return _dbContext.Customers.FirstOrDefault(u => u.EmailAddress == username);
        }

        public Volunteer GetVolunteerByID(int id)
        {
            return _dbContext.Volunteers.FirstOrDefault(u => u.ID == id);
        }

        public Volunteer GetVolunteerByUsername(string username)
        {
            return _dbContext.Volunteers.FirstOrDefault(u => u.EmailAddress == username);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _dbContext.Customers;
        }

        public IEnumerable<Volunteer> GetAllVolunteers()
        {
            return _dbContext.Volunteers;
        }
    }
}