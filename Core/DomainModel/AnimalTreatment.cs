using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DomainModel
{
    public class AnimalTreatment
    {
        public int AnimalID { get; set; }
        public Animal Animal { get; set; }

        public int TreatmentID { get; set; }
        public Treatment Treatment { get; set; }

        [Required]
        public string PerformedBy { get; set; }
        
        [Required]
        public DateTime PerformDate { get; set; }
    }
}