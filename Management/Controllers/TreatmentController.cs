using Core.DomainModel;
using DomainServices.Services;
using Management.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Management.Controllers
{
    public class TreatmentController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly ITreatmentService _treatmentService;

        public TreatmentController(IAnimalService animalService, ITreatmentService treatmentService)
        {
            _animalService = animalService;
            _treatmentService = treatmentService;
        }

        // GET: TreatmentController
        public IActionResult Index(int? animalId = null)
        {
            IEnumerable<Treatment> treatments;
            if (animalId != null)
            {
                ViewBag.AnimalName = _animalService.GetByID(animalId.Value).Name;
                treatments = _treatmentService.GetTreatmentsOfAnimal(animalId.Value);
            }
            else
            {
                treatments = _treatmentService.GetAll();
            }
            return View(treatments);
        }

        // GET: TreatmentController/Details/5
        public IActionResult Details(int id)
        {
            var treatment = _treatmentService.GetByID(id);
            return View(treatment);
        }

        // GET: TreatmentController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TreatmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                _treatmentService.Create(treatment);
                return RedirectToAction(nameof(Index));
            }
            return View(treatment);
        }

        // GET: TreatmentController/Edit/5
        public IActionResult Edit(int id)
        {
            var treatment = _treatmentService.GetByID(id);
            return View(treatment);
        }

        // POST: TreatmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                _treatmentService.Update(treatment);
                return RedirectToAction(nameof(Index));
            }
            return View(treatment);
        }

        // GET: TreatmentController/Delete/5
        public IActionResult Delete(int id)
        {
            var treatment = _treatmentService.GetByID(id);
            return View(treatment);
        }

        // POST: TreatmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Treatment treatment)
        {
            _treatmentService.Delete(treatment);
            return RedirectToAction(nameof(Index));
        }
    }
}