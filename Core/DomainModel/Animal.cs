using Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DomainModel
{
    /// <summary>
    /// POCO for animal
    /// </summary>
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
        public double Age { get; set; }

        [Display(Name = "Estimated age")]
        public int? EstimatedAge { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Cat or dog?")]
        public AnimalType AnimalType { get; set; }

        public string Breed { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public byte[] Picture { get; set; }

        public string ImageName { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Arrival date")]
        public DateTime? DateOfArrival { get; set; }

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

        public List<Treatment> Treatments { get; set; } = new List<Treatment>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

        [Required]
        [Display(Name = "Reason for putting it up for adoption?")]
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

        [NotMapped]
        public string PictureData { get; set; }
    }
}