using Core.DomainModel;
using DomainServices.Repositories;
using DomainServices.Services;
using System;
using System.Collections.Generic;

namespace Services
{
    public class TreatmentService : ITreatmentService
    {
        private readonly ITreatmentRepository _treatmentRepository;

        public TreatmentService(ITreatmentRepository treatmentRepository)
        {
            _treatmentRepository = treatmentRepository;
        }

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

        public void Update(Treatment treatment)
        {
            _treatmentRepository.Update(treatment);
        }
    }
}
