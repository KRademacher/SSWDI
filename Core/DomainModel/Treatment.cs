using Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DomainModel
{
    public class Treatment
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public TreatmentType TreatmentType { get; set; }

        public string Description { get; set; }

        public double Cost { get; set; }

        public int MinimumAge { get; set; }

        [Required]
        public string PerformedBy { get; set; }

        [Required]
        public DateTime PerformDate { get; set; }
    }
}