using Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DomainModel
{
    public class Volunteer : User
    {
        [Required]
        public DateTime DateOfBirth { get; set; }

        public Volunteer()
        {
            UserRole = UserRole.Volunteer;
        }
    }
}