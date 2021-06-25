using Core.DomainModel;
using DomainServices.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HttpData
{
    public class HttpAnimalRepository : IAnimalRepository
    {
        private readonly string apiBaseUrl = "https://localhost:44315";

        public void Create(Animal animal)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(animal);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    using HttpResponseMessage response = httpClient.PostAsync(apiBaseUrl + "/api/animal", content).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(Animal animal)
        {
            throw new InvalidOperationException("Delete operation not permitted.");
        }

        public IEnumerable<Animal> GetAll()
        {
            List<Animal> animals = new List<Animal>();

            using (var httpClient = new HttpClient())
            {
                using HttpResponseMessage response = httpClient.GetAsync(apiBaseUrl + "/api/animal").Result;
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                animals = JsonConvert.DeserializeObject<List<Animal>>(apiResponse);
            }
            return animals;
        }

        public IEnumerable<Animal> GetAllAvailableAnimals()
        {
            List<Animal> animals = new List<Animal>();

            using (var httpClient = new HttpClient())
            {
                using HttpResponseMessage response = httpClient.GetAsync(apiBaseUrl + "/api/animal").Result;
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                animals = JsonConvert.DeserializeObject<List<Animal>>(apiResponse);
            }
            return animals;
        }

        public Animal GetByID(int id)
        {
            Animal animal = new Animal();

            using (var httpClient = new HttpClient())
            {
                using HttpResponseMessage response = httpClient.GetAsync(apiBaseUrl + "/api/animal/" + id).Result;
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                animal = JsonConvert.DeserializeObject<Animal>(apiResponse);
            }
            return animal;
        }

        public void Update(Animal animal)
        {
            throw new InvalidOperationException("Update operation not permitted.");
        }
    }
}