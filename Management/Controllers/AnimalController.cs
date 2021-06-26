using Core.DomainModel;
using DomainServices.Repositories;
using DomainServices.Services;
using Management.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Management.Controllers
{
    [Authorize(Policy = "RequireVolunteer")]
    public class AnimalController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly ILodgingService _lodgingService;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AnimalController(
            IAnimalService animalService, 
            ILodgingService lodgingService,
            IUserRepository userRepository,
            IWebHostEnvironment hostEnvironment)
        {
            _animalService = animalService;
            _lodgingService = lodgingService;
            _userRepository = userRepository;
            _hostEnvironment = hostEnvironment;
        }

        // GET: AnimalController
        public IActionResult Index()
        {
            return View(_animalService.GetAll());
        }

        // GET: AnimalController/Details/5
        public IActionResult Details(int id)
        {
            var animal = _animalService.GetByID(id);
            if (animal.LodgingID != null)
            {
                animal.LodgingLocation = _lodgingService.GetByID(animal.LodgingID.Value);
            }
            if (animal.Picture != null)
            {
                string pictureBase64Data = Convert.ToBase64String(animal.Picture);
                animal.PictureData = string.Format("data:/image/jpg;base64,{0}", pictureBase64Data);
            }
            if (animal.AdoptedByID != null)
            {
                animal.AdoptedBy = _userRepository.GetCustomerByID(animal.AdoptedByID.Value);
            }
            return View(animal);
        }

        // GET: AnimalController/Create
        public IActionResult Create()
        {
            Animal animal = new Animal()
            {
                DateOfArrival = DateTime.Now
            };
            return View(animal);
        }

        // POST: AnimalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Animal animal)
        {
            var availableLodgings = _lodgingService.GetCompatibleLodgings(animal.AnimalType, animal.Gender, animal.IsNeutered);
            if (availableLodgings.Count() == 0)
            {
                ViewBag.Error = "Can't register animal: No available lodges.";
                return View(animal);
            }
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(animal.ImageFile.FileName))
                {
                    animal.ImageName = animal.ImageFile.FileName;
                }
                animal = _animalService.Create(animal);

                // Place animal in first available lodge
                var lodge = availableLodgings.First();
                _lodgingService.AddAnimalToLodge(lodge, animal);
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        // GET: AnimalController/Edit/5
        public IActionResult Edit(int id)
        {
            var animal = _animalService.GetByID(id);
            return View(animal);
        }

        // POST: AnimalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Animal animal)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(animal.ImageFile.FileName))
                {
                    animal.ImageName = animal.ImageFile.FileName;
                }
                _animalService.Update(animal);
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        // GET: AnimalController/Delete/5
        public IActionResult Delete(int id)
        {
            var animal = _animalService.GetByID(id);
            return View(animal);
        }

        // POST: AnimalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Animal animal)
        {
            _animalService.Delete(animal);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult PlaceAnimalInLodging(int id)
        {
            var animal = _animalService.GetByID(id);
            var viewModel = new AnimalViewModel()
            {
                Animal = animal,
                AnimalType = animal.AnimalType,
                AllLodgings = _lodgingService.GetAll().ToList(),
                AvailableLodgings = _lodgingService.GetCompatibleLodgings(id).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult PlaceAnimalInLodging(AnimalViewModel viewModel)
        {
            var animal = _animalService.GetByID(viewModel.Animal.ID);
            var lodge = _lodgingService.GetByID(viewModel.Lodging.ID);
            // Check if the lodge isn't the same. If it is, nothing needs to happen
            if (animal.LodgingID == null || animal.LodgingID.Value != lodge.ID)
            {
                _lodgingService.AddAnimalToLodge(lodge, animal);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult RemoveAnimalFromLodge(int id)
        {
            var animal = _animalService.GetByID(id);
            if (animal.LodgingID == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var lodge = _lodgingService.GetByID(animal.LodgingID.Value);
            var viewModel = new AnimalViewModel()
            {
                Animal = animal,
                AnimalType = animal.AnimalType,
                Lodging = lodge
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult RemoveAnimalFromLodge(AnimalViewModel viewModel)
        {
            var animal = _animalService.GetByID(viewModel.Animal.ID);
            var lodge = _lodgingService.GetByID(viewModel.Lodging.ID);
            _lodgingService.RemoveAnimalFromLodge(lodge, animal);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult RegisterAdoption(int id)
        {
            Animal animal = _animalService.GetByID(id);
            List<Customer> customers = _userRepository.GetAllCustomers().ToList();
            var viewModel = new AdoptionViewModel()
            {
                Animal = animal,
                Customers = customers,
                DateOfAdoption = DateTime.Now
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult RegisterAdoption(AdoptionViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.AdopteeName) && viewModel.Customer.ID == 0)
            {
                ModelState.AddModelError(nameof(viewModel.AdopteeName), "Either a customer or name has to be filled in.");
                return View(viewModel);
            }
            var animal = _animalService.GetByID(viewModel.Animal.ID);
            if (viewModel.Customer.ID == 0)
            {
                animal.AdopteeName = viewModel.AdopteeName;
            }
            else if (string.IsNullOrWhiteSpace(viewModel.AdopteeName))
            {
                var customer = _userRepository.GetCustomerByID(viewModel.Customer.ID);
                animal.AdoptedByID = viewModel.Customer.ID;
                animal.AdoptedBy = customer;
                    
            }
            if (animal.LodgingID != null)
            {
                var lodge = _lodgingService.GetByID(animal.LodgingID.Value);
                _lodgingService.RemoveAnimalFromLodge(lodge, animal);
            }
            animal.Adoptable = false;
            animal.DateOfAdoption = viewModel.DateOfAdoption;
            _animalService.Update(animal);
            return RedirectToAction(nameof(Index));
        }
    }
}