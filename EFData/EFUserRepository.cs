using Core.DomainModel;
using DomainServices.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace EFData
{
    public class EFUserRepository : EFGenericRepository<User>, IUserRepository
    {
        public EFUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Customer FindCustomerByID(int id)
        {
            return _dbContext.Customers.FirstOrDefault(u => u.ID == id);
        }

        public Customer FindCustomerByUserName(string username)
        {
            return _dbContext.Customers.FirstOrDefault(u => u.EmailAddress == username);
        }

        public Volunteer FindVolunteerByID(int id)
        {
            return _dbContext.Volunteers.FirstOrDefault(u => u.ID == id);
        }

        public Volunteer FindVolunteerByUsername(string username)
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