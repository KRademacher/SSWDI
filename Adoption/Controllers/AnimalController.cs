using Core.DomainModel;
using Core.Enums;
using DomainServices.Repositories;
using DomainServices.Services;
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
        private readonly ILodgingService _lodgingService;

        public AnimalController(IAnimalRepository animalRepository, ILodgingService lodgingService)
        {
            _animalRepository = animalRepository;
            _lodgingService = lodgingService;
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
            var availableLodges = _lodgingService.GetCompatibleLodgings(animal.AnimalType, animal.Gender, animal.IsNeutered);
            if (availableLodges.Count() == 0)
            {
                ViewBag.Error = "Cannot register animal, shelter has no room.";
                return View(animal);
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
                    animal = _animalRepository.Create(animal);

                    var lodge = availableLodges.First();
                    _lodgingService.AddAnimalToLodge(lodge, animal);

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