﻿using Core.DomainModel;
using Core.Enums;
using DomainServices.Repositories;
using DomainServices.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public void AddComment(Comment comment)
        {
            var animal = GetByID(comment.AnimalID);
            animal.Comments.Add(comment);
            _animalRepository.Update(animal);
        }

        public void AddTreatment(Treatment treatment)
        {
            try
            {
                var animal = GetByID(treatment.AnimalID);
                if (animal.DateOfPassing != null && animal.DateOfPassing.Value < treatment.PerformDate)
                {
                    throw new InvalidOperationException("Cannot perform treatment on dead animal.");
                }
                if (animal.LodgingID == null)
                {
                    throw new InvalidOperationException("Cannot add treatment to animal not in lodging.");
                }
                if (((treatment.TreatmentType &
                    (TreatmentType.Euthanasia |
                    TreatmentType.Surgery |
                    TreatmentType.Vaccination |
                    TreatmentType.Chipping)) != 0) &&
                    string.IsNullOrWhiteSpace(treatment.Description))
                {
                    if (treatment.TreatmentType.HasFlag(TreatmentType.Chipping))
                    {
                        throw new InvalidOperationException("Entering GUID is required with chipping.");
                    }
                    throw new InvalidOperationException("Description is required with this treatment.");
                }

                animal.Treatments.Add(treatment);
                _animalRepository.Update(animal);
            }
            catch (InvalidOperationException iOE)
            {
                throw iOE;
            }
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

        public void DeleteTreatment(Treatment treatment)
        {
            var animal = GetByID(treatment.AnimalID);
            if (animal.Treatments.Contains(treatment))
            {
                animal.Treatments.Remove(treatment);
                _animalRepository.Update(animal);
            }
        }

        public IEnumerable<Animal> GetAll()
        {
            return _animalRepository.GetAll();
        }

        public IEnumerable<Animal> GetAllAvailableAnimals()
        {
            return _animalRepository.GetAllAvailableAnimals();
        }

        public Animal GetByID(int id)
        {
            return _animalRepository.GetByID(id);
        }

        public IEnumerable<Comment> GetComments(int id)
        {
            var animal = GetByID(id);
            return animal.Comments;
        }

        public IEnumerable<Treatment> GetTreatments(int id)
        {
            var animal = GetByID(id);
            return animal.Treatments;
        }

        public void RemoveImage(Animal animal, string wwwRootPath)
        {
            if (animal.ImageName != null)
            {
                var imagePath = Path.Combine(wwwRootPath, "images", animal.ImageName);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
        }

        public void Update(Animal animal)
        {
            try
            {
                animal.Age = CalculateAnimalAge(animal);
                if (animal.Age == -1)
                {
                    throw new InvalidOperationException("Age can't be less than 0.");
                }
                _animalRepository.Update(animal);
            }
            catch (InvalidOperationException iOE)
            {
                throw iOE;
            }
        }

        public void UpdateTreatment(Treatment treatment)
        {
            var animal = GetByID(treatment.AnimalID);
            var oldTreatment = animal.Treatments.FirstOrDefault(t => t.ID == treatment.ID);
            if (oldTreatment != null)
            {
                if (oldTreatment.TreatmentType != treatment.TreatmentType)
                {
                    oldTreatment.TreatmentType = treatment.TreatmentType;
                }
                if (oldTreatment.Description != treatment.Description)
                {
                    oldTreatment.Description = treatment.Description;
                }
                if (oldTreatment.Cost != treatment.Cost)
                {
                    oldTreatment.Cost = treatment.Cost;
                }
                if (oldTreatment.MinimumAge != treatment.MinimumAge)
                {
                    oldTreatment.MinimumAge = treatment.MinimumAge;
                }
                if (oldTreatment.PerformDate != treatment.PerformDate)
                {
                    oldTreatment.PerformDate = treatment.PerformDate;
                }
                if (oldTreatment.PerformedBy != treatment.PerformedBy)
                {
                    oldTreatment.PerformedBy = treatment.PerformedBy;
                }
                _animalRepository.Update(animal);
            }
        }

        public async Task<string> UploadImage(Animal animal, string wwwRootPath)
        {
            string fileName = Path.GetFileNameWithoutExtension(animal.ImageFile.FileName);
            string extension = Path.GetExtension(animal.ImageFile.FileName);
            // Make filename unique
            fileName = fileName + DateTime.Now.ToString("yyMMdd_HHmmss") + extension;
            animal.ImageName = fileName;

            string path = Path.Combine(wwwRootPath + "/images", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await animal.ImageFile.CopyToAsync(fileStream);
            }
            return fileName;
        }

        public int CalculateAnimalAge(Animal animal)
        {
            // Throw error if animal's age is below 0 for whatever reason
            if (animal.Age < 0 || animal.EstimatedAge < 0)
            {
                throw new InvalidOperationException("Age can't be less than 0.");
            }

            if (animal.EstimatedAge == null && animal.DateOfBirth == null)
            {
                throw new InvalidOperationException("Either the estimated age or the date of birth has to be filled in.");
            }

            // Throw error if both date of birth and estimated age are filled in
            if (animal.EstimatedAge != null && animal.DateOfBirth != null)
            {
                throw new InvalidOperationException("Animal can't have both an estimated age and an actual age.");
            }

            // If estimated age has a value but date of birth doesn't, return estimated age
            if (animal.EstimatedAge != null && animal.DateOfBirth == null)
            {
                return animal.EstimatedAge.Value;
            }

            // If date of birth has a value but estimate age doesn't calculate age based on date of birth
            if (animal.DateOfBirth != null && animal.EstimatedAge == null)
            {
                //Calculate the age of the animal
                var today = DateTime.Today;
                var age = today.Year - animal.DateOfBirth.Value.Year;

                //Account for leap years
                if (animal.DateOfBirth.Value.Date > today.AddYears(-age))
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
