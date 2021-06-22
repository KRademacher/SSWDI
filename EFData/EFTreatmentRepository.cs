using Core.DomainModel;
using DomainServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFData
{
    public class EFTreatmentRepository : EFGenericRepository<Treatment>, ITreatmentRepository
    {
        public EFTreatmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void Delete(Treatment treatment)
        {
            _dbContext.Treatments.Remove(treatment);
            _dbContext.SaveChanges();
        }

        public void Update(Treatment treatment)
        {
            Treatment currentTreatment = _dbContext.Treatments.FirstOrDefault(t => t.ID == treatment.ID);
            if (currentTreatment != null)
            {
                _dbContext.Entry(currentTreatment).CurrentValues.SetValues(treatment);
                _dbContext.SaveChanges();
            }
        }
    }
}