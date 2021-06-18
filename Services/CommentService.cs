using Core.DomainModel;
using DomainServices.Repositories;
using DomainServices.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class CommentService : ICommentService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ICommentRepository _commentRepository;

        public CommentService(IAnimalRepository animalRepository, ICommentRepository commentRepository)
        {
            _animalRepository = animalRepository;
            _commentRepository = commentRepository;
        }

        public void Create(Comment comment)
        {
            _commentRepository.Create(comment);
        }

        public void Delete(Comment comment)
        {
            _commentRepository.Delete(comment);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public Comment GetByID(int id)
        {
            return _commentRepository.GetByID(id);
        }

        public IEnumerable<Comment> GetRelatedComments(int animalId)
        {
            var animal = _animalRepository.GetByID(animalId);
            return animal.Comments;
        }

        public void Update(Comment comment)
        {
            _commentRepository.Update(comment);
        }
    }
}
