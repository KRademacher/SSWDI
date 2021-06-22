using Core.DomainModel;
using DomainServices.Repositories;
using System.Linq;

namespace EFData
{
    public class EFUserRepository : EFGenericRepository<User>, IUserRepository
    {
        public EFUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public User FindByUserName(string username)
        {
            var users = GetAll();
            return users.FirstOrDefault(u => u.EmailAddress == username);
        }
    }
}