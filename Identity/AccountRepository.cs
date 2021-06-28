using Identity.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> RegisterCustomer(ApplicationUser applicationUser)
        {
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
            }
            return result;
        }

        public async Task<IdentityResult> RegisterVolunteer(ApplicationUser applicationUser)
        {
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
            }
            return result;
        }
    }
}