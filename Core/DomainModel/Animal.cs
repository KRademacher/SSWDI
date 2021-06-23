using Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DomainModel
{
    public class Animal
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name= "Date of birth")]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public int Age { get; set; }

        [Display(Name = "Estimated age")]
        public int EstimatedAge { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Cat or dog?")]
        public AnimalType AnimalType { get; set; }

        public string Breed { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public string Picture { get; set; }

        public string ImageName { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Arrival date")]
        public DateTime DateOfArrival { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Adoption date")]
        public DateTime? DateOfAdoption { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Passing date")]
        public DateTime? DateOfPassing { get; set; }

        [Required]
        [Display(Name = "Is the animal neutered?")]
        public bool IsNeutered { get; set; }

        [Required]
        [Display(Name = "Is the animal child friendly?")]
        public  ChildFriendly IsChildFriendly { get; set; }

        public List<AnimalTreatment> Treatments { get; set; } = new List<AnimalTreatment>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

        [Required]
        [Display(Name = "Why was the animal put up for adoption?")]
        public string LeavingReason { get; set; }

        [Required]
        [Display(Name = "Is the animal adoptable?")]
        public bool Adoptable { get; set; }

        public int? AdoptedByID { get; set; }

        [ForeignKey("AdoptedByID")]
        public Customer AdoptedBy { get; set; }

        public int? LodgingID { get; set; }
        
        [ForeignKey("LodgingID")]
        public Lodging LodgingLocation { get; set; }

        public List<InterestedAnimal> InterestedAdoptees { get; set; } = new List<InterestedAnimal>();

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}