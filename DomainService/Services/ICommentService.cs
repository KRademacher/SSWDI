using Core.DomainModel;
using DomainService.Repositories;
using System.Collections.Generic;

namespace DomainService.Services
{
    public interface ICommentService : ICommentRepository
    {
        public IEnumerable<Comment> GetRelatedComments(int animalId);
    }
}