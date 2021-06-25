using DomainServices.Repositories;
using DomainServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adoption.Controllers
{
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

        [Authorize(Policy = "RequireVolunteerOrCustomer")]
        [Route("InterestedAnimal/Create/{animalId:int}")]
        public IActionResult Create(int animalId)
        {
            var customer = _userService.FindCustomerByUserName(User.Identity.Name);
            var animal = _animalRepository.GetByID(animalId);
            try
            {
                _interestedAnimalRepository.Create(animal, customer);
                return RedirectToAction(nameof(Index), "Animal");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize(Policy = "RequireVolunteerOrCustomer")]
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
