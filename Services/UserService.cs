using Core.DomainModel;
using DomainServices.Repositories;
using DomainServices.Services;
using Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(IUserRepository userRepository, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public User Create(User user)
        {
            return _userRepository.Create(user);
        }

        public Customer GetCustomerByID(int id)
        {
            return _userRepository.GetCustomerByID(id);
        }

        public Customer GetCustomerByUserName(string username)
        {
            return _userRepository.GetCustomerByUserName(username);
        }

        public Volunteer GetVolunteerByID(int id)
        {
            return _userRepository.GetVolunteerByID(id);
        }

        public Volunteer GetVolunteerByUsername(string username)
        {
            return _userRepository.GetVolunteerByUsername(username);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _userRepository.GetAllCustomers();
        }

        public IEnumerable<Volunteer> GetAllVolunteers()
        {
            return _userRepository.GetAllVolunteers();
        }

        public User GetByID(int id)
        {
            return _userRepository.GetByID(id);
        }

        public async Task<IdentityResult> RegisterCustomer(Customer customer, ApplicationUser applicationUser)
        {
            applicationUser.Id = Guid.NewGuid().ToString();
            applicationUser.UserName = customer.EmailAddress;
            applicationUser.Email = customer.EmailAddress;

            var result = await _userManager.CreateAsync(applicationUser);
            if (result.Succeeded)
            {
                bool roleExists = await _roleManager.RoleExistsAsync("Customer");
                if (!roleExists)
                {
                    var newRole = new IdentityRole("Customer");
                    await _roleManager.CreateAsync(newRole);
                }

                var customerRole = await _roleManager.FindByNameAsync("Customer");
                await _userManager.AddToRoleAsync(applicationUser, customerRole.Name);
                Create(customer);
            }
            return result;
        }

        public async Task<IdentityResult> RegisterVolunteer(Volunteer volunteer, ApplicationUser applicationUser)
        {
            applicationUser.Id = Guid.NewGuid().ToString();
            applicationUser.UserName = volunteer.EmailAddress;
            applicationUser.Email = volunteer.EmailAddress;

            var result = await _userManager.CreateAsync(applicationUser);
            if (result.Succeeded)
            {
                bool roleExists = await _roleManager.RoleExistsAsync("Volunteer");
                if (!roleExists)
                {
                    var newRole = new IdentityRole("Volunteer");
                    await _roleManager.CreateAsync(newRole);
                }

                var volunteerRole = await _roleManager.FindByNameAsync("Volunteer");
                await _userManager.AddToRoleAsync(applicationUser, volunteerRole.Name);
                Create(volunteer);
            }
            return result;
        }
    }
}