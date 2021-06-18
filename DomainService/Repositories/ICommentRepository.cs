using Core.DomainModel;

namespace DomainService.Repositories
{
    public interface ICommentRepository : ICrudRepository<Comment>
    {
        void SaveComment(Comment comment);
    }
}