using Core.DomainModel;

namespace DomainServices.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User FindByUserName(string username);
    }
}