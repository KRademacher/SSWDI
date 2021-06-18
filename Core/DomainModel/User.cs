using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.DomainModel
{
    public abstract class User
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public UserRole UserRole { get; protected set; }
    }
}