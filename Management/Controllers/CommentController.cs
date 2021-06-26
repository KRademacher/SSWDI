using Core.DomainModel;
using DomainServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Management.Controllers
{
    [Authorize(Policy = "RequireVolunteer")]
    public class CommentController : Controller
    {
        private readonly IAnimalService _animalService;

        public CommentController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        // GET: CommentController
        [Route("Comment/Index/{animalId:int}")]
        public IActionResult Index(int animalId)
        {
            var animal = _animalService.GetByID(animalId);
            ViewBag.Animal = animal.Name;
            ViewBag.AnimalId = animalId;
            return View(animal.Comments);
        }

        // GET: CommentController/Create
        [Route("Comment/Create/{animalId:int}")]
        public IActionResult Create(int animalId)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var comment = new Comment()
            {
                AnimalID = animalId,
                Author = username,
                Date = DateTime.Now
            };
            return View(comment);
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Comment/Create/{animalId:int}")]
        public IActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _animalService.AddComment(comment);
                //return RedirectToAction(nameof(Index));
                return Redirect($"~/Comment/Index/{comment.AnimalID}");
            }
            return View(comment);
        }
    }
}