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
    /// Business Rule 1:
    /// Het maximaal te plaatsen dieren wordt niet overschreden.
    /// </summary>
    public class BusinessRule1
    {
        [Fact]
        public void AnimalCanBeAddedToLodge()
        {
            // Arrange
            Mock<IAnimalRepository> animalRepository = new Mock<IAnimalRepository>();
            Mock<ILodgingRepository> lodgingRepository = new Mock<ILodgingRepository>();

            ILodgingService lodgingService = new LodgingService(animalRepository.Object, lodgingRepository.Object);

            Animal dog = new Animal()
            {
                ID = 1,
                Name = "Gizmo",
                DateOfBirth = new DateTime(2007, 11, 2),
                Age = 14,
                EstimatedAge = 14,
                Description = "Best dog ever",
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
                Adoptable = true
            };

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

            //Setup
            lodgingRepository.Setup(l => l.GetByID(lodging.ID)).Returns(lodging);
            animalRepository.Setup(a => a.GetByID(dog.ID)).Returns(dog);

            // Act
            lodgingService.AddAnimalToLodge(lodging, dog);

            //Assert
            lodgingRepository.Verify(x => x.Update(lodging), Times.Once());
            lodgingRepository.Verify(x => x.Update(lodging));

            animalRepository.Verify(x => x.Update(dog), Times.Once());
            animalRepository.Verify(x => x.Update(dog));
        }

        [Fact]
        public void AnimalShouldNotBeAddedToFullLodge()
        {
            // Arrange
            Mock<IAnimalRepository> animalRepository = new Mock<IAnimalRepository>();
            Mock<ILodgingRepository> lodgingRepository = new Mock<ILodgingRepository>();

            ILodgingService lodgingService = new LodgingService(animalRepository.Object, lodgingRepository.Object);

            Animal dog = new Animal()
            {
                ID = 1,
                Name = "Gizmo",
                DateOfBirth = new DateTime(2007, 11, 2),
                Age = 14,
                EstimatedAge = 14,
                Description = "Best dog ever",
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
                Adoptable = true
            };

            Lodging fullLodge = new Lodging()
            {
                ID = 1,
                Description = "This is a lodge",
                LodgingType = LodgingType.GroupMale,
                AnimalType = AnimalType.Dog,
                CurrentCapacity = 10,
                MaxCapacity = 10,
                LodgingAnimals = new List<Animal>()
            };

            //Setup
            lodgingRepository.Setup(l => l.GetByID(fullLodge.ID)).Returns(fullLodge);
            animalRepository.Setup(a => a.GetByID(dog.ID)).Returns(dog);

            // Act
            var exception = Assert.Throws<InvalidOperationException>(() => lodgingService.AddAnimalToLodge(fullLodge, dog));

            // Assert
            Assert.Equal("Services", exception.Source);
            Assert.Equal("System.InvalidOperationException", exception.GetType().ToString());
            Assert.Equal("Chosen lodging is at max capacity.", exception.Message);
        }
    }
}