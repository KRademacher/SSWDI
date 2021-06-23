using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public List<AnimalTreatment> AnimalTreatments { get; set; } = new List<AnimalTreatment>();
    }
}