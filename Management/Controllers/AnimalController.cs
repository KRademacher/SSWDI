﻿using Core.DomainModel;
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
        private readonly ITreatmentService _treatmentService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AnimalController(
            IAnimalService animalService, 
            ILodgingService lodgingService,
            ITreatmentService treatmentService,
            IWebHostEnvironment hostEnvironment)
        {
            _animalService = animalService;
            _lodgingService = lodgingService;
            _treatmentService = treatmentService;
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
            if (animal.LodgingID.Value != lodge.ID)
            {
                _lodgingService.AddAnimalToLodge(lodge, animal);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult RemoveAnimalFromLodge(int id)
        {
            var animal = _animalService.GetByID(id);
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
        public IActionResult AddTreatmentToAnimal(int id)
        {
            var animal = _animalService.GetByID(id);
            var viewModel = new TreatmentViewModel()
            {
                Animal = animal,
                Treatments = _treatmentService.GetAll().ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddTreatmentToAnimal(TreatmentViewModel viewModel)
        {
            var animal = _animalService.GetByID(viewModel.Animal.ID);
            var treatment = _treatmentService.GetByID(viewModel.Treatment.ID);
            var animalTreatment = new AnimalTreatment()
            {
                AnimalID = animal.ID,
                Animal = animal,
                TreatmentID = treatment.ID,
                Treatment = treatment,
                PerformedBy = viewModel.PerformedBy,
                PerformDate = viewModel.PerformDate
            };
            _animalService.AddTreatment(animalTreatment);
            return RedirectToAction(nameof(Index));
        }
    }
}