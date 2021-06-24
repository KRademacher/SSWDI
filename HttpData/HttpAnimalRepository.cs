using Core.DomainModel;
using DomainServicesHttp.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpData
{
    public class HttpAnimalRepository : IAnimalHttpRepository
    {
        private readonly string apiBaseUrl = "https://localhost:44315";

        public async Task<Animal> Add(Animal animal)
        {
            try
            {
                Animal receivedAnimal = new Animal();

                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(animal), Encoding.UTF8, "application/json");

                    using HttpResponseMessage response = await httpClient.PostAsync(apiBaseUrl + "/api/animal", content);
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedAnimal = JsonConvert.DeserializeObject<Animal>(apiResponse);
                }
                return receivedAnimal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Animal>> GetAll()
        {
            List<Animal> animals = new List<Animal>();

            using (var httpClient = new HttpClient())
            {
                using HttpResponseMessage response = await httpClient.GetAsync(apiBaseUrl + "/api/animal");
                string apiResponse = await response.Content.ReadAsStringAsync();
                animals = JsonConvert.DeserializeObject<List<Animal>>(apiResponse);
            }

            return animals;
        }

        public async Task<IEnumerable<Animal>> GetAvailableAnimals()
        {
            List<Animal> animals = new List<Animal>();

            using (var httpClient = new HttpClient())
            {
                using HttpResponseMessage response = await httpClient.GetAsync(apiBaseUrl + "/api/animal/available");
                string apiResponse = await response.Content.ReadAsStringAsync();
                animals = JsonConvert.DeserializeObject<List<Animal>>(apiResponse);
            }

            return animals;
        }

        public async Task<Animal> GetByID(int id)
        {
            Animal animal = new Animal();

            using (var httpClient = new HttpClient())
            {
                using HttpResponseMessage response = await httpClient.GetAsync(apiBaseUrl + "/api/animal/" + id);
                string apiResponse = await response.Content.ReadAsStringAsync();
                animal = JsonConvert.DeserializeObject<Animal>(apiResponse);
            }
            return animal;
        }

        public async Task<IEnumerable<Animal>> GetInterestedAnimals(ClaimsPrincipal user)
        {
            List<Animal> animals = new List<Animal>();

            using (var httpClient = new HttpClient())
            {
                // TODO: Find cleaner way to do this
                var builder = new UriBuilder(apiBaseUrl + "/api/animal");
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["User"] = user.Identity.Name;
                builder.Query = query.ToString();
                string url = builder.ToString();

                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    animals = JsonConvert.DeserializeObject<List<Animal>>(apiResponse);
                }
            }
            return animals;
        }
    }
}