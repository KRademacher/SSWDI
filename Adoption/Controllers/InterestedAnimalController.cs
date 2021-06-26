using DomainServices.Repositories;
using DomainServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Adoption.Controllers
{
    [Authorize(Policy = "RequireVolunteerOrCustomer")]
    public class InterestedAnimalController : Controller
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IInterestedAnimalRepository _interestedAnimalRepository;
        private readonly IUserService _userService;

        public InterestedAnimalController(IAnimalRepository animalRepository,
            IInterestedAnimalRepository interestedAnimalRepository, IUserService userService)
        {
            _animalRepository = animalRepository;
            _interestedAnimalRepository = interestedAnimalRepository;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var customer = _userService.FindCustomerByUserName(User.Identity.Name);
            return View(_interestedAnimalRepository.GetAll(customer.ID));
        }

        [Route("InterestedAnimal/Create/{animalId:int}")]
        public IActionResult Create(int animalId)
        {
            var customer = _userService.FindCustomerByUserName(User.Identity.Name);
            var animal = _animalRepository.GetByID(animalId);
            if (_interestedAnimalRepository.Get(customer.ID, animal.ID) != null)
            {
                TempData["Error"] = "You've already shown interest to this animal.";
            }
            if (_interestedAnimalRepository.GetAll(customer.ID).ToList().Count == 3)
            {
                TempData["Error"] = "You've shown interest to three animals.\n" +
                    "Please remove an animals from your list before adding another one.";
            }
            try
            {
                _interestedAnimalRepository.Create(animal, customer);
            }
            catch (Exception e)
            {
                throw e;
            }
            return RedirectToAction(nameof(Index), "Animal");
        }

        [Route("InterestedAnimal/Delete/{animalId:int}")]
        public IActionResult Delete(int animalId)
        {
            var customer = _userService.FindCustomerByUserName(User.Identity.Name);
            var animal = _animalRepository.GetByID(animalId);
            try
            {
                _interestedAnimalRepository.Delete(animal, customer);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}