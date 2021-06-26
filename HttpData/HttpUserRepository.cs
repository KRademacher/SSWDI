using Core.DomainModel;
using DomainServices.Repositories;
using System;
using System.Collections.Generic;

namespace HttpData
{
    public class HttpUserRepository : IUserRepository
    {
        public User Create(User user)
        {
            return Globals.HttpPost(user, "/api/register");
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
            throw new InvalidOperationException("Operation not permitted");
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