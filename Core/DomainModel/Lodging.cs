using Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DomainModel
{
    public class Lodging
    {
        [Required]
        [Key]
        public int ID { get; set; }

        public string Description { get; set; }

        [Required]
        public LodgingType LodgingType { get; set; }

        [Required]
        public AnimalType AnimalType { get; set; }

        public int MaxCapacity { get; set; }

        public int CurrentCapacity { get; set; }

        public List<Animal> LodgingAnimals { get; set; } = new List<Animal>();
    }
}