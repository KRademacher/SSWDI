using Core.DomainModel;
using DomainServices.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HttpData
{
    public class HttpUserRepository : IUserRepository
    {
        public User Create(User user)
        {
            Customer customer;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(user);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    using HttpResponseMessage response = httpClient.PostAsync(Globals.ApiBaseUrl + "/api/register", content).Result;
                    response.EnsureSuccessStatusCode();
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    customer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return customer;
        }

        public IEnumerable<User> GetAll()
        {
            throw new InvalidOperationException("Operation not permitted");
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            throw new InvalidOperationException("Operation not permitted");
        }

        public IEnumerable<Volunteer> GetAllVolunteers()
        {
            throw new InvalidOperationException("Operation not permitted");
        }

        public User GetByID(int id)
        {
            throw new InvalidOperationException("Operation not permitted");
        }

        public Customer GetCustomerByID(int id)
        {
            throw new InvalidOperationException("Operation not permitted");
        }

        public Customer GetCustomerByUserName(string username)
        {
            return Globals.HttpGet<Customer>($"/api/account/{username}");
        }

        public Volunteer GetVolunteerByID(int id)
        {
            throw new InvalidOperationException("Operation not permitted");
        }

        public Volunteer GetVolunteerByUsername(string username)
        {
            throw new InvalidOperationException("Operation not permitted");
        }
    }
}