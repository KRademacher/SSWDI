using Core.DomainModel;
using DomainServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
            return View(animal.Comments);
        }

        // GET: CommentController/Details/5
        public IActionResult Details(int id, int animalId)
        {
            var animal = _animalService.GetByID(animalId);
            var comment = animal.Comments.FirstOrDefault(c => c.ID == id);
            return View(comment);
        }

        // GET: CommentController/Create
        [Route("Comment/Create/{animalId:int}")]
        public IActionResult Create(int animalId)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var comment = new Comment()
            {
                AnimalID = animalId,
                Author = username
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
                return RedirectToAction(nameof(Index), "Animal");
            }
            return View(comment);
        }

        // GET: CommentController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}