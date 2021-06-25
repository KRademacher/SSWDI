using Core.DomainModel;
using DomainServices.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HttpData
{
    public class HttpInterestedAnimalRepository : IInterestedAnimalRepository
    {
        public InterestedAnimal Create(Animal animal, Customer customer)
        {
            InterestedAnimal interestedAnimal = new InterestedAnimal()
            {
                Animal = animal,
                AnimalID = animal.ID,
                Customer = customer,
                CustomerID = customer.ID
            };
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(interestedAnimal);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    using HttpResponseMessage response = httpClient.PostAsync(Globals.ApiBaseUrl + "/api/interest", content).Result;
                    response.EnsureSuccessStatusCode();
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<InterestedAnimal>(apiResponse);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(Animal animal, Customer customer)
        {
            InterestedAnimal interestedAnimal = new InterestedAnimal()
            {
                Animal = animal,
                AnimalID = animal.ID,
                Customer = customer,
                CustomerID = customer.ID
            };
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(interestedAnimal);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    using HttpResponseMessage response = httpClient.DeleteAsync(Globals.ApiBaseUrl + $"/api/interest/{interestedAnimal.CustomerID}/{interestedAnimal.AnimalID}").Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Animal> GetAll(int customerId)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using HttpResponseMessage response = httpClient.GetAsync(Globals.ApiBaseUrl + $"/api/interest/{customerId}").Result;
                    response.EnsureSuccessStatusCode();
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<IEnumerable<Animal>>(apiResponse);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
