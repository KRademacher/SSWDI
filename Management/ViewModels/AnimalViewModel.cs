using Core.DomainModel;
using Core.Enums;
using System.Collections.Generic;

namespace Management.ViewModels
{
    public class AnimalViewModel
    {
        public Animal Animal { get; set; }

        public AnimalType AnimalType { get; set; }

        public Lodging Lodging { get; set; }

        public List<Lodging> AllLodgings { get; set; }

        public List<Lodging> AvailableLodgings { get; set; }
    }
}