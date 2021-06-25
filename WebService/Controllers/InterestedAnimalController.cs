﻿using Core.DomainModel;
using DomainServices.Repositories;
using DomainServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [ApiController]
    [Route("/api/interest")]
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

        [HttpGet("{id:int}")]
        public IActionResult GetAll(int id)
        {
            return Ok(_interestedAnimalRepository.GetAll(id));
        }

        [HttpGet("{customerId:int}/{animalId:int}")]
        public IActionResult Get(int customerId, int animalId)
        {
            return Ok(_interestedAnimalRepository.Get(customerId, animalId));
        }

        [HttpPost]
        public IActionResult Create(InterestedAnimal interestedAnimal)
        {
            _interestedAnimalRepository.Create(interestedAnimal.Animal, interestedAnimal.Customer);
            return Ok(interestedAnimal);
        }

        [HttpDelete("{customerId:int}/{animalId:int}")]
        public IActionResult Delete(int customerId, int animalId)
        {
            var customer = _userService.FindCustomerByID(customerId);
            var animal = _animalRepository.GetByID(animalId);
            _interestedAnimalRepository.Delete(animal, customer);
            return Ok();
        }
    }
}