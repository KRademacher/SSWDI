using Core.DomainModel;
using DomainServices.Repositories;
using System;
using System.Collections.Generic;

namespace HttpData
{
    public class HttpAnimalRepository : IAnimalRepository
    {
        public Animal Create(Animal animal)
        {
            return Globals.HttpPost(animal, "/api/animal");
        }

        public void Delete(Animal animal)
        {
            throw new InvalidOperationException("Delete operation not permitted.");
        }

        public IEnumerable<Animal> GetAll()
        {
            return Globals.HttpGet<List<Animal>>("/api/animal");
        }

        public IEnumerable<Animal> GetAllAvailableAnimals()
        {
            return Globals.HttpGet<List<Animal>>("/api/animal");
        }

        public Animal GetByID(int id)
        {
            return Globals.HttpGet<Animal>($"/api/animal/{id}");
        }

        public void Update(Animal animal)
        {
            Globals.HttpPut(animal, $"/api/animal/{animal.ID}");
        }
    }
}