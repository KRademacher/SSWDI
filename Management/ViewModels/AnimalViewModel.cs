using Core.DomainModel;
using Core.Enums;
using System.Collections.Generic;

namespace Management.ViewModels
{
    public class AnimalViewModel
    {
        public Animal Animal { get; set; }

        public AnimalType AnimalType { get; set; }

        public Lodging Lodge { get; set; }

        public List<Lodging> AvailableLodgings { get; set; }
    }
}