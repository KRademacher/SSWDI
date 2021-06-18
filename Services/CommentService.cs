using Core.DomainModel;
using DomainService.Repositories;
using DomainService.Services;
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

        public void SaveComment(Comment comment)
        {
            _commentRepository.SaveComment(comment);
        }
    }
}
