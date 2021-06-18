using Core.DomainModel;
using DomainService.Repositories;
using DomainService.Services;
using System;
using System.Collections.Generic;

namespace Services
{
    public class TreatmentService : ITreatmentService
    {
        private readonly ITreatmentRepository _treatmentRepository;

        public void Create(Treatment treatment)
        {
            _treatmentRepository.Create(treatment);
        }

        public void Delete(Treatment treatment)
        {
            _treatmentRepository.Delete(treatment);
        }

        public IEnumerable<Treatment> GetAll()
        {
            return _treatmentRepository.GetAll();
        }

        public Treatment GetByID(int id)
        {
            return _treatmentRepository.GetByID(id);
        }

        public void SaveTreatment(Treatment treatment)
        {
            _treatmentRepository.SaveTreatment(treatment);
        }
    }
}
