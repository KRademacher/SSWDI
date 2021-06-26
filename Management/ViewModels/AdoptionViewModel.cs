using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Management.ViewModels
{
    public class AdoptionViewModel
    {
        public Animal Animal{ get; set; }
        public Customer Customer { get; set; }
        public List<Customer> Customers { get; set; }
        public string AdopteeName { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateOfAdoption { get; set; }
    }
}