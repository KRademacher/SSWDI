using Core.DomainModel;
using Core.Enums;
using DomainServices.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Adoption.Controllers
{
    public class AnimalController : Controller
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public IActionResult Index(AnimalType? animalType, Gender? gender, ChildFriendly? childFriendly)
        {
            var animals = _animalRepository.GetAllAvailableAnimals();
            if (animalType != null)
            {
                animals = animals.Where(a => a.AnimalType == animalType);
            }
            if (gender != null)
            {
                animals = animals.Where(a => a.Gender == gender);
            }
            if (childFriendly != null)
            {
                animals = animals.Where(a => a.IsChildFriendly == childFriendly);
            }
            return View(animals);
        }

        [Authorize(Policy = "RequireVolunteerOrCustomer")]
        [HttpGet]
        public IActionResult Create()
        {
            Animal animal = new Animal()
            {
                DateOfArrival = DateTime.Now
            };
            return View(animal);
        }

        [Authorize(Policy = "RequireVolunteerOrCustomer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Animal animal)
        {
            if (string.IsNullOrWhiteSpace(animal.LeavingReason))
            {
                ModelState.AddModelError(nameof(animal.LeavingReason), "Please give a reason for giving away the animal");
            }
            if (animal.ImageFile != null)
            {
                MemoryStream stream = new MemoryStream();
                animal.ImageFile.CopyTo(stream);
                animal.Picture = stream.ToArray();
                animal.ImageFile = null;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _animalRepository.Create(animal);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException e)
                {
                    throw e;
                }
            }
            return View(animal);
        }
    }
}