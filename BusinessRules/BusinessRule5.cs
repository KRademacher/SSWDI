using Core.DomainModel;
using Core.Enums;
using DomainServices.Repositories;
using Moq;
using Services;
using System;
using Xunit;

namespace BusinessRules
{
    /// <summary>
    /// Business Rule 5:
    /// Voor een dier moet ofwel de leeftijd uit worden gerekend aan de 
    /// hand de geboortedatum of de geschatte leeftijd moet ingevuld zijn.
    /// </summary>
    public class BusinessRule5
    {
        [Fact]
        public void AgeShouldBeCalculatedFromDateOfBirth()
        {
            // Arrange
            Mock<IAnimalRepository> animalRepository = new Mock<IAnimalRepository>();
            AnimalService animalService = new AnimalService(animalRepository.Object);

            Animal dog = new Animal()
            {
                ID = 1,
                Name = "Gizmo",
                DateOfBirth = new DateTime(2007, 11, 2),
                EstimatedAge = null,
                AnimalType = AnimalType.Dog,
                Gender = Gender.Male,
                IsNeutered = true,
                IsChildFriendly = ChildFriendly.Yes,
                Adoptable = true
            };

            // Act
            var result = animalService.CalculateAnimalAge(dog);

            // Assert
            Assert.Equal(13, result);
        }

        [Fact]
        public void AnimalShouldHaveEstimatedAgeIfDateOfBirthIsUnknown()
        {
            // Arrange
            Mock<IAnimalRepository> animalRepository = new Mock<IAnimalRepository>();
            AnimalService animalService = new AnimalService(animalRepository.Object);

            Animal dogWithEstimatedAge = new Animal()
            {
                ID = 1,
                Name = "Gizmo",
                EstimatedAge = 14,
                AnimalType = AnimalType.Dog,
                Gender = Gender.Male,
                IsNeutered = true,
                IsChildFriendly = ChildFriendly.Yes,
                Adoptable = true
            };

            Animal dogWithoutEstimatedAge = new Animal()
            {
                ID = 2,
                Name = "Gizmo",
                AnimalType = AnimalType.Dog,
                Gender = Gender.Male,
                IsNeutered = true,
                IsChildFriendly = ChildFriendly.Yes,
                Adoptable = true
            };

            // Act
            var firstResult = animalService.CalculateAnimalAge(dogWithEstimatedAge);
            var secondResult = Assert.Throws<InvalidOperationException>(() => 
                    animalService.CalculateAnimalAge(dogWithoutEstimatedAge));

            // Assert
            Assert.Equal(14, firstResult);

            Assert.Equal("Services", secondResult.Source);
            Assert.Equal("System.InvalidOperationException", secondResult.GetType().ToString());
            Assert.Equal("Either the estimated age or the date of birth has to be filled in.", secondResult.Message);
        }

        [Fact]
        public void AnimalCannotHaveBothDateOfBirthAndEstimatedAge()
        {
            // Arrange
            Mock<IAnimalRepository> animalRepository = new Mock<IAnimalRepository>();
            AnimalService animalService = new AnimalService(animalRepository.Object);

            Animal dog = new Animal()
            {
                ID = 1,
                Name = "Gizmo",
                DateOfBirth = new DateTime(2007, 11, 2),
                EstimatedAge = 14,
                AnimalType = AnimalType.Dog,
                Gender = Gender.Male,
                IsNeutered = true,
                IsChildFriendly = ChildFriendly.Yes,
                Adoptable = true
            };

            var result = Assert.Throws<InvalidOperationException>(() =>
                    animalService.CalculateAnimalAge(dog));

            Assert.Equal("Services", result.Source);
            Assert.Equal("System.InvalidOperationException", result.GetType().ToString());
            Assert.Equal("Animal can't have both an estimated age and an actual age.", result.Message);
        }

        [Fact]
        public void AnimalCannotHaveAgeBelowZero()
        {
            // Arrange
            Mock<IAnimalRepository> animalRepository = new Mock<IAnimalRepository>();
            AnimalService animalService = new AnimalService(animalRepository.Object);

            Animal dog = new Animal()
            {
                ID = 1,
                Name = "Gizmo",
                EstimatedAge = -1,
                AnimalType = AnimalType.Dog,
                Gender = Gender.Male,
                IsNeutered = true,
                IsChildFriendly = ChildFriendly.Yes,
                Adoptable = true
            };

            var result = Assert.Throws<InvalidOperationException>(() =>
                    animalService.CalculateAnimalAge(dog));

            Assert.Equal("Services", result.Source);
            Assert.Equal("System.InvalidOperationException", result.GetType().ToString());
            Assert.Equal("Age can't be less than 0.", result.Message);
        }
    }
}