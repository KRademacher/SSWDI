﻿using Core.DomainModel;
using DomainService.Repositories;
using DomainService.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public void AddComment(Animal animal, Comment comment)
        {
            animal.Comments.Add(comment);
        }

        public void Create(Animal animal)
        {
            try
            {
                animal.Age = CalculateAnimalAge(animal);
                if (animal.Age == -1)
                {
                    throw new InvalidOperationException("Age can't be less than 0.");
                }
                _animalRepository.Create(animal);
            }
            catch (InvalidOperationException iOE)
            {
                throw iOE;
            }
        }

        public void Delete(Animal animal)
        {
            _animalRepository.Delete(animal);
        }

        public IEnumerable<Animal> GetAll()
        {
            return _animalRepository.GetAll();
        }

        public IEnumerable<Animal> GetAllAvailableAnimals()
        {
            return _animalRepository.GetAll().Where(a => a.DateOfPassing == null).ToList();
        }

        public Animal GetByID(int id)
        {
            return _animalRepository.GetByID(id);
        }

        public void SaveAnimal(Animal animal)
        {
            try
            {
                animal.Age = CalculateAnimalAge(animal);
                if (animal.Age == -1)
                {
                    throw new InvalidOperationException("Age can't be less than 0.");
                }
                _animalRepository.SaveAnimal(animal);
            }
            catch (InvalidOperationException iOE)
            {
                throw iOE;
            }
        }

        private int CalculateAnimalAge(Animal animal)
        {
            // Throw error if animal's age is below 0 for whatever reason
            if (animal.Age < 0)
            {
                throw new InvalidOperationException("Age can't be less than 0.");
            }

            // Throw error if both date of birth and estimated age are filled in
            if (animal.EstimatedAge != 0 && animal.DateOfBirth != null)
            {
                throw new InvalidOperationException("Animal can't have both an estimated age and an actual age.");
            }

            // If estimated age has a value but date of birth doesn't, return estimated age
            if (animal.EstimatedAge != 0 && animal.DateOfBirth == null)
            {
                return animal.EstimatedAge;
            }

            // If date of birth has a value but estimate age doesn't calculate age based on date of birth
            if (animal.DateOfBirth != null && animal.EstimatedAge == 0)
            {
                //Calculate the age of the animal
                var today = DateTime.Today;
                var age = today.Year - animal.DateOfBirth.Year;

                //Account for leap years
                if (animal.DateOfBirth.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
            // return -1 to catch any errors and bugs
            return -1;
        }
    }
}