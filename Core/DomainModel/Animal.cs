using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DomainModel
{
    public class Animal
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        public int Age { get; set; }

        public int EstimateAge { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public AnimalType AnimalType { get; set; }

        public string Breed { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public string Picture { get; set; }

        [Required]
        public DateTime DateOfArrival { get; set; }

        public DateTime DateOfAdoption { get; set; }

        public DateTime DateOfPassing { get; set; }

        [Required]
        public bool IsNeutered { get; set; }

        [Required]
        public  ChildFriendly IsChildFriendly { get; set; }

        public List<Treatment> Treatments { get; set; } = new List<Treatment>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

        [Required]
        public string LeavingReason { get; set; }

        [Required]
        public bool Adoptable { get; set; }

        public string AdoptedBy { get; set; }
    }
}