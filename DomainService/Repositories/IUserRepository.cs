using Core.DomainModel;

namespace DomainServices.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Customer FindCustomerByID(int id);
        Customer FindCustomerByUserName(string username);
        Volunteer FindVolunteerByID(int id);
        Volunteer FindVolunteerByUsername(string username);
    }
}