using Core.DomainModel;
using DomainServices.Services;
using Management.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Management.Controllers
{
    [Authorize(Policy = "RequireVolunteer")]
    public class AnimalController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly ILodgingService _lodgingService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AnimalController(IAnimalService animalService, 
            ILodgingService lodgingService,
            IWebHostEnvironment hostEnvironment)
        {
            _animalService = animalService;
            _lodgingService = lodgingService;
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
            return View(animal);
        }

        // GET: AnimalController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnimalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Animal animal)
        {
            if (ModelState.IsValid)
            {
                if (animal.ImageFile != null)
                {
                    // We need the wwwRootPath which is relative to the project, not the specific service.
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    animal.ImageName = await _animalService.UploadImage(animal, wwwRootPath);
                }
                _animalService.Create(animal);
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
        public async Task<IActionResult> Edit(Animal animal)
        {
            if (ModelState.IsValid)
            {
                if (animal.ImageFile != null)
                {
                    // We need the wwwRootPath which is relative to the project, not the specific service.
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    _animalService.RemoveImage(animal, wwwRootPath);
                    animal.ImageName = await _animalService.UploadImage(animal, wwwRootPath);
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
            string wwwRootPath = _hostEnvironment.WebRootPath;
            _animalService.RemoveImage(animal, wwwRootPath);
            _animalService.Delete(animal);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult PlaceAnimal(int id)
        {
            var animal = _animalService.GetByID(id);
            var viewModel = new AnimalViewModel()
            {
                Animal = animal,
                AnimalType = animal.AnimalType,
                AvailableLodgings = _lodgingService.GetCompatibleLodgings(id).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult PlaceAnimal(AnimalViewModel viewModel)
        {
            _lodgingService.AddAnimalToLodge(viewModel.Lodge, viewModel.Animal);
            return RedirectToAction(nameof(Index));
        }
    }
}
