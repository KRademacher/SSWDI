using Core.DomainModel;
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

        public void AddTreatment(AnimalTreatment animalTreatment)
        {
            try
            {
                if (animalTreatment.Animal.DateOfPassing != null ||
                    animalTreatment.Animal.DateOfPassing.Value > animalTreatment.PerformDate)
                {
                    throw new InvalidOperationException("Cannot perform treatment on dead animal.");
                }
                if (animalTreatment.Animal.LodgingID == null)
                {
                    throw new InvalidOperationException("Cannot add treatment to animal not in lodging.");
                }

                animalTreatment.Animal.Treatments.Add(animalTreatment);
                _animalRepository.Update(animalTreatment.Animal);
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

        public IEnumerable<Comment> GetComments(int id)
        {
            var animal = GetByID(id);
            return animal.Comments;
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
