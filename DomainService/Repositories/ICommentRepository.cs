using Core.DomainModel;

namespace DomainServices.Repositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        void Update(Comment comment);

        void Delete(Comment comment);
    }
}