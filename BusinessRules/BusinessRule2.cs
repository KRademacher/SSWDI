using Core.DomainModel;
using Core.Enums;
using DomainServices.Repositories;
using DomainServices.Services;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BusinessRules
{
    /// <summary>
    /// Business Rule 2:
    /// Niet gesteriliseerde/gecastreerde dieren kunnen niet qua 
    /// geslacht gemengd geplaatst worden in hetzelfde bedrijf.
    /// </summary>
    public class BusinessRule2
    {
        [Fact]
        public void ServiceShouldReturnCompatibleLodges()
        {
            // Arrange
            Mock<IAnimalRepository> animalRepository = new Mock<IAnimalRepository>();
            Mock<ILodgingRepository> lodgingRepository = new Mock<ILodgingRepository>();

            ILodgingService lodgingService = new LodgingService(animalRepository.Object, lodgingRepository.Object);

            List<Lodging> lodgings = new List<Lodging>();

            Animal dog = new Animal()
            {
                ID = 1,
                Name = "Gizmo",
                DateOfBirth = new DateTime(2007, 11, 2),
                Age = 14,
                EstimatedAge = 14,
                Description = "Neutered male dog",
                AnimalType = AnimalType.Dog,
                Breed = "Labrador Retriever/Golden Retriever mix",
                Gender = Gender.Male,
                DateOfArrival = new DateTime(2021, 2, 2),
                DateOfAdoption = null,
                DateOfPassing = null,
                IsNeutered = true,
                IsChildFriendly = ChildFriendly.Yes,
                LeavingReason = "Would never",
                Adoptable = true
            };

            Lodging compatibleLodge1 = new Lodging()
            {
                ID = 1,
                Description = "This is a group lodge for male dogs",
                LodgingType = LodgingType.GroupMale,
                AnimalType = AnimalType.Dog,
                CurrentCapacity = 6,
                MaxCapacity = 10,
                LodgingAnimals = new List<Animal>()
            };
            lodgings.Add(compatibleLodge1);

            Lodging compatibleLodge2 = new Lodging()
            {
                ID = 2,
                Description = "This is a individual lodge for dogs",
                LodgingType = LodgingType.Individual,
                AnimalType = AnimalType.Dog,
                CurrentCapacity = 6,
                MaxCapacity = 10,
                LodgingAnimals = new List<Animal>()
            };
            lodgings.Add(compatibleLodge2);

            Lodging incompatibleLodge = new Lodging()
            {
                ID = 3,
                Description = "This is a group lodge for male cats",
                LodgingType = LodgingType.GroupMale,
                AnimalType = AnimalType.Cat,
                CurrentCapacity = 6,
                MaxCapacity = 10,
                LodgingAnimals = new List<Animal>()
            };
            lodgings.Add(incompatibleLodge);

            //Setup
            lodgingRepository.Setup(l => l.GetAll()).Returns(lodgings);
            animalRepository.Setup(a => a.GetByID(dog.ID)).Returns(dog);

            // Act
            var result = lodgingService.GetCompatibleLodgings(dog.ID).ToList();

            //Assert
            Assert.Contains(result, item => item.ID == compatibleLodge1.ID);
            Assert.Contains(result, item => item.ID == compatibleLodge2.ID);
            Assert.DoesNotContain(result, item => item.ID == incompatibleLodge.ID);
        }

        [Fact]
        public void ServiceShouldNotReturnIncompatibleLodges()
        {
            // Arrange
            Mock<IAnimalRepository> animalRepository = new Mock<IAnimalRepository>();
            Mock<ILodgingRepository> lodgingRepository = new Mock<ILodgingRepository>();

            ILodgingService lodgingService = new LodgingService(animalRepository.Object, lodgingRepository.Object);

            List<Lodging> lodgings = new List<Lodging>();

            Animal dog = new Animal()
            {
                ID = 1,
                Name = "Gizmo",
                DateOfBirth = new DateTime(2007, 11, 2),
                Age = 14,
                EstimatedAge = 14,
                Description = "Not neutered male dog",
                AnimalType = AnimalType.Dog,
                Breed = "Labrador Retriever/Golden Retriever mix",
                Gender = Gender.Male,
                DateOfArrival = new DateTime(2021, 2, 2),
                DateOfAdoption = null,
                DateOfPassing = null,
                IsNeutered = false,
                IsChildFriendly = ChildFriendly.Yes,
                LeavingReason = "Would never",
                Adoptable = true
            };

            Lodging compatibleLodge = new Lodging()
            {
                ID = 1,
                Description = "This is a group lodge for male dogs",
                LodgingType = LodgingType.GroupMale,
                AnimalType = AnimalType.Dog,
                CurrentCapacity = 6,
                MaxCapacity = 10,
                LodgingAnimals = new List<Animal>()
            };
            lodgings.Add(compatibleLodge);

            Lodging incompatibleLodge = new Lodging()
            {
                ID = 2,
                Description = "This is a group lodge for female dogs",
                LodgingType = LodgingType.GroupFemale,
                AnimalType = AnimalType.Dog,
                CurrentCapacity = 6,
                MaxCapacity = 10,
                LodgingAnimals = new List<Animal>()
            };
            lodgings.Add(incompatibleLodge);

            //Setup
            lodgingRepository.Setup(l => l.GetAll()).Returns(lodgings);
            animalRepository.Setup(a => a.GetByID(dog.ID)).Returns(dog);

            // Act
            var result = lodgingService.GetCompatibleLodgings(dog.ID).ToList();

            //Assert
            Assert.Contains(result, item => item.ID == compatibleLodge.ID);
            Assert.DoesNotContain(result, item => item.ID == incompatibleLodge.ID);
        }
    }
}