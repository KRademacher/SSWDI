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
    /// Business Rule 4:
    /// Bij een aantal behandelingen is een toelichting verplicht.
    /// </summary>
    public class BusinessRule4
    {
        [Fact]
        public void CanCreateTreatmentWithNotRequiredDescription()
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
                Treatments = new List<Treatment>(),
                LodgingID = 1
            };

            Treatment castration = new Treatment()
            {
                ID = 1,
                Description = "castration",
                TreatmentType = TreatmentType.Castration,
                Cost = 15,
                MinimumAge = 12,
                PerformedBy = "Kevin",
                PerformDate = DateTime.Now,
                PerformedOn = dog,
                AnimalID = dog.ID
            };

            Treatment sterilisation = new Treatment()
            {
                ID = 1,
                Description = "sterilisation",
                TreatmentType = TreatmentType.Sterilisation,
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
            animalService.AddTreatment(castration);
            animalService.AddTreatment(sterilisation);

            // Assert
            animalRepository.Verify(a => a.Update(dog), Times.Exactly(2));
            animalRepository.Verify(a => a.Update(dog));
            Assert.Contains(dog.Treatments, item => item.ID == castration.ID);
            Assert.Contains(dog.Treatments, item => item.ID == sterilisation.ID);
        }

        [Fact]
        public void CannotCreateTreatmentWithMissingRequiredDescription()
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
                Treatments = new List<Treatment>(),
                LodgingID = 1
            };

            Treatment euthanasia = new Treatment()
            {
                ID = 1,
                Description = null,
                TreatmentType = TreatmentType.Euthanasia,
                Cost = 15,
                MinimumAge = 12,
                PerformedBy = "Kevin",
                PerformDate = DateTime.Now,
                PerformedOn = dog,
                AnimalID = dog.ID
            };

            Treatment surgery = new Treatment()
            {
                ID = 1,
                Description = null,
                TreatmentType = TreatmentType.Surgery,
                Cost = 15,
                MinimumAge = 12,
                PerformedBy = "Kevin",
                PerformDate = DateTime.Now,
                PerformedOn = dog,
                AnimalID = dog.ID
            };

            Treatment vaccination = new Treatment()
            {
                ID = 1,
                Description = null,
                TreatmentType = TreatmentType.Vaccination,
                Cost = 15,
                MinimumAge = 12,
                PerformedBy = "Kevin",
                PerformDate = DateTime.Now,
                PerformedOn = dog,
                AnimalID = dog.ID
            };

            Treatment chipping = new Treatment()
            {
                ID = 1,
                Description = null,
                TreatmentType = TreatmentType.Chipping,
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
            var euthanasiaException = Assert.Throws<InvalidOperationException>(() => animalService.AddTreatment(euthanasia));
            var surgeryException = Assert.Throws<InvalidOperationException>(() => animalService.AddTreatment(surgery));
            var vaccinationException = Assert.Throws<InvalidOperationException>(() => animalService.AddTreatment(vaccination));
            var chippingException = Assert.Throws<InvalidOperationException>(() => animalService.AddTreatment(chipping));

            // Assert
            Assert.Equal("Services", euthanasiaException.Source);
            Assert.Equal("Services", surgeryException.Source);
            Assert.Equal("Services", vaccinationException.Source);
            Assert.Equal("Services", chippingException.Source);

            Assert.Equal("System.InvalidOperationException", euthanasiaException.GetType().ToString());
            Assert.Equal("System.InvalidOperationException", surgeryException.GetType().ToString());
            Assert.Equal("System.InvalidOperationException", vaccinationException.GetType().ToString());
            Assert.Equal("System.InvalidOperationException", chippingException.GetType().ToString());

            Assert.Equal("Description is required with this treatment.", euthanasiaException.Message);
            Assert.Equal("Description is required with this treatment.", surgeryException.Message);
            Assert.Equal("Description is required with this treatment.", vaccinationException.Message);
            Assert.Equal("Entering GUID is required with chipping.", chippingException.Message);
        }
    }
}