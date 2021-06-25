using Adoption.ViewModels;
using AutoMapper;
using Core.DomainModel;
using DomainServices.Services;
using Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adoption.Controllers
{
    public class AccountController : Controller
    {
        private readonly PasswordHasher<ApplicationUser> _passwordHasher;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public List<AuthenticationScheme> ExternalLogins { get; set; }

        public AccountController(
            PasswordHasher<ApplicationUser> passwordHasher,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserService userService, IMapper mapper)
        {
            _passwordHasher = passwordHasher;
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return RedirectToPage("/Account/Login", new { Area = "Identity" });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var appUser = new Customer()
                {
                    EmailAddress = model.EmailAddress,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    StreetName = model.StreetName,
                    HouseNumber = model.HouseNumber,
                    HouseNumberAddition = model.HouseNumberAddition,
                    ZipCode = model.ZipCode,
                    City = model.City,
                    Country = model.Country
                };
                var mappedUser = _mapper.Map<Customer, ApplicationUser>(appUser);
                mappedUser.PasswordHash = _passwordHasher.HashPassword(mappedUser, model.Password);
                var result = await _userService.RegisterCustomer(appUser, mappedUser);

                if (result.Succeeded)
                {
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { emailAddress = model.EmailAddress, returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(mappedUser, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View();
        }
    }
}