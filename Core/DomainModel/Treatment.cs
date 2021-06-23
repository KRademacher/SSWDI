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

        [Required]
        public string PerformedBy { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime PerformDate
        {
            get
            {
                return _performDate ?? DateTime.Now;
            }
            set
            {
                _performDate = value;
            }
        }

        public int AnimalID { get; set; }
        [ForeignKey("AnimalID")]
        public Animal PerformedOn { get; set; }

        private DateTime? _performDate;
    }
}