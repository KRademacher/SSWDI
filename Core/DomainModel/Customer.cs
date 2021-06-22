using Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DomainModel
{
    public class Customer : User
    {
        [Required]
        public int RegistrationNumber { get; set; }

        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public string HouseNumberAddition { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public List<Animal> AdoptedAnimals { get; set; } = new List<Animal>();
        public List<InterestedAnimal> AnimalsInterestedIn { get; set; } = new List<InterestedAnimal>();

        public Customer()
        {
            UserRole = UserRole.Customer;
        }
    }
}