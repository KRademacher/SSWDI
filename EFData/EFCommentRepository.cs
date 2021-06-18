using Core.DomainModel;
using DomainServices.Repositories;
using System.Linq;

namespace EFData
{
    public class EFCommentRepository : EFGenericRepository<Comment>, ICommentRepository
    {
        public EFCommentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void Delete(Comment comment)
        {
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();
        }

        public void Update(Comment comment)
        {
            Comment currentComment = _dbContext.Comments.FirstOrDefault(c => c.ID == comment.ID);
            if (currentComment != null)
            {
                _dbContext.Entry(currentComment).CurrentValues.SetValues(comment);
                _dbContext.SaveChanges();
            }
        }
    }
}