using Core.DomainModel;
using Core.Enums;
using DomainServices.Repositories;
using DomainServices.Services;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessRules
{
    /// <summary>
    /// Business Rule 3:
    /// Een behandeling kan niet in worden gevoerd als het dier nog
    /// niet in het asiel is opgenomen of nadat het dier overleden is.
    /// </summary>
    public class BusinessRule3
    {
        [Fact]
        public void CanAddTreatmentToAliveShelteredAnimal()
        {
            // Arrange
            Mock<IAnimalRepository> animalRepository = new Mock<IAnimalRepository>();

            IAnimalService animalService = new AnimalService(animalRepository.Object);

            Lodging lodging = new Lodging()
            {
                ID = 1,
                Description = "This is a lodge",
                LodgingType = LodgingType.GroupMale,
                AnimalType = AnimalType.Dog,
                CurrentCapacity = 6,
                MaxCapacity = 10,
                LodgingAnimals = new List<Animal>()
            };

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
                Picture = "gizmo.jpg",
                DateOfArrival = new DateTime(2021, 2, 2),
                DateOfAdoption = null,
                DateOfPassing = null,
                IsNeutered = true,
                IsChildFriendly = ChildFriendly.Yes,
                LeavingReason = "Would never",
                Adoptable = true,
                Treatments = new List<Treatment>(),
                LodgingLocation = lodging,
                LodgingID = lodging.ID
            };

            Treatment rabiesShot = new Treatment()
            {
                ID = 1,
                Description = "Rabies shot",
                TreatmentType = TreatmentType.Vaccination,
                Cost = 15,
                MinimumAge = 12,
                PerformedBy = "Kevin",
                PerformDate = DateTime.Now,
                PerformedOn = dog,
                AnimalID = dog.ID
            };

            // Setup
            animalRepository.Setup(a => a.GetByID(dog.ID)).Returns(dog);

            // Act
            animalService.AddTreatment(rabiesShot);

            // Assert
            animalRepository.Verify(a => a.Update(dog), Times.Once());
            animalRepository.Verify(a => a.Update(dog));
            Assert.Contains(dog.Treatments, item => item.ID == rabiesShot.ID);
        }

        [Fact]
        public void CannotAddTreatmentToDeadAnimal()
        {
            // Arrange
            Mock<IAnimalRepository> animalRepository = new Mock<IAnimalRepository>();

            IAnimalService animalService = new AnimalService(animalRepository.Object);

            Lodging lodging = new Lodging()
            {
                ID = 1,
                Description = "This is a lodge",
                LodgingType = LodgingType.GroupMale,
                AnimalType = AnimalType.Dog,
                CurrentCapacity = 6,
                MaxCapacity = 10,
                LodgingAnimals = new List<Animal>()
            };

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
                Picture = "gizmo.jpg",
                DateOfArrival = new DateTime(2021, 2, 2),
                DateOfAdoption = null,
                DateOfPassing = new DateTime(2021, 4, 1),
                IsNeutered = true,
                IsChildFriendly = ChildFriendly.Yes,
                LeavingReason = "Would never",
                Adoptable = true,
                Treatments = new List<Treatment>(),
                LodgingLocation = lodging,
                LodgingID = lodging.ID
            };

            Treatment rabiesShot = new Treatment()
            {
                ID = 1,
                Description = "Rabies shot",
                TreatmentType = TreatmentType.Vaccination,
                Cost = 15,
                MinimumAge = 12,
                PerformedBy = "Kevin",
                PerformDate = DateTime.Now,
                PerformedOn = dog,
                AnimalID = dog.ID
            };

            // Setup
            animalRepository.Setup(a => a.GetByID(dog.ID)).Returns(dog);

            // Act
            var exception = Assert.Throws<InvalidOperationException>(() => animalService.AddTreatment(rabiesShot));

            // Assert
            Assert.Equal("Services", exception.Source);
            Assert.Equal("System.InvalidOperationException", exception.GetType().ToString());
            Assert.Equal("Cannot perform treatment on dead animal.", exception.Message);
        }

        [Fact]
        public void CannotAddTreatmentToUnshelteredAnimal()
        {
            // Arrange
            Mock<IAnimalRepository> animalRepository = new Mock<IAnimalRepository>();

            IAnimalService animalService = new AnimalService(animalRepository.Object);

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
                Picture = "gizmo.jpg",
                DateOfArrival = new DateTime(2021, 2, 2),
                DateOfAdoption = null,
                DateOfPassing = null,
                IsNeutered = true,
                IsChildFriendly = ChildFriendly.Yes,
                LeavingReason = "Would never",
                Adoptable = true,
                Treatments = new List<Treatment>()
            };

            Treatment rabiesShot = new Treatment()
            {
                ID = 1,
                Description = "Rabies shot",
                TreatmentType = TreatmentType.Vaccination,
                Cost = 15,
                MinimumAge = 12,
                PerformedBy = "Kevin",
                PerformDate = DateTime.Now,
                PerformedOn = dog,
                AnimalID = dog.ID
            };

            // Setup
            animalRepository.Setup(a => a.GetByID(dog.ID)).Returns(dog);

            // Act
            var exception = Assert.Throws<InvalidOperationException>(() => animalService.AddTreatment(rabiesShot));

            // Assert
            Assert.Equal("Services", exception.Source);
            Assert.Equal("System.InvalidOperationException", exception.GetType().ToString());
            Assert.Equal("Cannot add treatment to animal not in lodging.", exception.Message);
        }
    }
}