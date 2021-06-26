using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> RegisterCustomer(ApplicationUser applicationUser);
        Task<IdentityResult> RegisterVolunteer(ApplicationUser applicationUser);
    }
}