using Core.DomainModel;
using Core.Enums;
using DomainServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Management.Controllers
{
    [Authorize(Policy = "RequireVolunteer")]
    public class TreatmentController : Controller
    {
        private readonly IAnimalService _animalService;

        public TreatmentController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [Route("Animal/{animalId:int}/Treatment")]
        public IActionResult Index(int animalId)
        {
            var animal = _animalService.GetByID(animalId);
            ViewBag.AnimalId = animalId;
            ViewBag.AnimalName = _animalService.GetByID(animalId).Name;
            ViewBag.IsAnimalAdopted = (animal.AdoptedByID != null || !string.IsNullOrWhiteSpace(animal.AdopteeName));
            return View(_animalService.GetTreatments(animalId));
        }

        [Route("Animal/{animalId:int}/Treatment/Details/{id:int}")]
        public IActionResult Details(int id, int animalId)
        {
            ViewBag.AnimalId = animalId;
            var animal = _animalService.GetByID(animalId);
            var treatment = animal.Treatments.FirstOrDefault(t => t.ID == id);
            return View(treatment);
        }

        [Route("Animal/{animalId:int}/Treatment/Create")]
        public IActionResult Create(int animalId)
        {
            var treatment = new Treatment()
            {
                AnimalID = animalId,
                PerformedOn = _animalService.GetByID(animalId)
            };
            return View(treatment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Animal/{animalId:int}/Treatment/Create")]
        public IActionResult Create(Treatment treatment)
        {
            if (treatment.TreatmentType == TreatmentType.Euthanasia || 
                treatment.TreatmentType == TreatmentType.Surgery ||
                treatment.TreatmentType == TreatmentType.Vaccination ||
                treatment.TreatmentType == TreatmentType.Chipping)
            {
                if (treatment.TreatmentType == TreatmentType.Chipping && 
                    string.IsNullOrWhiteSpace(treatment.Description))
                {
                    ModelState.AddModelError(nameof(treatment.Description), "Entering GUID is required with chipping.");
                }
                if (string.IsNullOrWhiteSpace(treatment.Description))
                {
                    ModelState.AddModelError(nameof(treatment.Description), "Description is required with this treatment.");
                }
            }
            else
            {
                Animal animal = _animalService.GetByID(treatment.AnimalID);
                if (treatment.MinimumAge < 6 || animal.Age < 0.5f)
                {
                    ModelState.AddModelError(nameof(treatment.MinimumAge), "Castration can only be done when the animal is older than 6 months.");
                }
            }
            if (ModelState.IsValid)
            {
                _animalService.AddTreatment(treatment);
                return Redirect($"~/Animal/{treatment.AnimalID}/Treatment");
            }
            return View(treatment);
        }

        [Route("Animal/{animalId:int}/Treatment/Edit/{id:int}")]
        public IActionResult Edit(int id, int animalId)
        {
            ViewBag.AnimalId = animalId;
            var animal = _animalService.GetByID(animalId);
            var treatment = animal.Treatments.FirstOrDefault(t => t.ID == id);
            return View(treatment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Animal/{animalId:int}/Treatment/Edit/{id:int}")]
        public IActionResult Edit(Treatment treatment)
        {
            ViewBag.AnimalId = treatment.AnimalID;
            if (treatment.TreatmentType == TreatmentType.Euthanasia ||
                treatment.TreatmentType == TreatmentType.Surgery ||
                treatment.TreatmentType == TreatmentType.Vaccination ||
                treatment.TreatmentType == TreatmentType.Chipping)
            {
                if (treatment.TreatmentType == TreatmentType.Chipping &&
                    string.IsNullOrWhiteSpace(treatment.Description))
                {
                    ModelState.AddModelError(nameof(treatment.Description), "Entering GUID is required with chipping.");
                }
                if (string.IsNullOrWhiteSpace(treatment.Description))
                {
                    ModelState.AddModelError(nameof(treatment.Description), "Description is required with this treatment.");
                }
            }
            else
            {
                Animal animal = _animalService.GetByID(treatment.AnimalID);
                if (treatment.MinimumAge < 6 || animal.Age < 0.5f)
                {
                    ModelState.AddModelError(nameof(treatment.MinimumAge), "Castration can only be done when the animal is older than 6 months.");
                }
            }
            if (ModelState.IsValid)
            {
                _animalService.UpdateTreatment(treatment);
                return Redirect($"~/Animal/{treatment.AnimalID}/Treatment");
            }
            return View(treatment);
        }

        [Route("Animal/{animalId:int}/Treatment/Delete/{id:int}")]
        public IActionResult Delete(int id, int animalId)
        {
            ViewBag.AnimalId = animalId;
            var animal = _animalService.GetByID(animalId);
            var treatment = animal.Treatments.FirstOrDefault(t => t.ID == id);
            return View(treatment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Animal/{animalId:int}/Treatment/Delete/{id:int}")]
        public IActionResult Delete(Treatment treatment, int animalId)
        {
            var animal = _animalService.GetByID(animalId);
            var animalTreatment = animal.Treatments.FirstOrDefault(t => t.ID == treatment.ID);
            _animalService.DeleteTreatment(animalTreatment);
            return Redirect($"~/Animal/{animalTreatment.AnimalID}/Treatment");
        }
    }
}