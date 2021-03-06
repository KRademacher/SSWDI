using Core.DomainModel;
using DomainServices.Repositories;
using System.Collections.Generic;

namespace DomainServices.Services
{
    public interface IAnimalService : IAnimalRepository
    {
        void AddComment(Comment comment);
        IEnumerable<Treatment> GetTreatments(int id);
        void AddTreatment(Treatment treatment);
        void UpdateTreatment(Treatment treatment);
        void DeleteTreatment(Treatment treatment);
        IEnumerable<Comment> GetComments(int id);
    }
}