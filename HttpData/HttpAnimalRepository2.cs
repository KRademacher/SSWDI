using Core.DomainModel;
using DomainServices.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HttpData
{
    public class HttpAnimalRepository2 : IAnimalRepository
    {
        private readonly string apiBaseUrl = "https://localhost:44315";

        public void Create(Animal animal)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(animal), Encoding.UTF8, "application/json");
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
            throw new InvalidOperationException();
        }

        public IEnumerable<Animal> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Animal> GetAllAvailableAnimals()
        {
            throw new NotImplementedException();
        }

        public Animal GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Animal animal)
        {
            throw new NotImplementedException();
        }
    }
}