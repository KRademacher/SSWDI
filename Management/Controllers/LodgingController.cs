using Core.DomainModel;
using DomainServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers
{
    public class LodgingController : Controller
    {
        private readonly ILodgingService _lodgingService;

        public LodgingController(ILodgingService lodgingService)
        {
            _lodgingService = lodgingService;
        }

        // GET: LodgingController
        public IActionResult Index()
        {
            return View(_lodgingService.GetAll());
        }

        // GET: LodgingController/Details/5
        public IActionResult Details(int id)
        {
            var lodging = _lodgingService.GetByID(id);
            return View(lodging);
        }

        // GET: LodgingController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LodgingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Lodging lodging)
        {
            if (ModelState.IsValid)
            {
                _lodgingService.Create(lodging);
                return RedirectToAction(nameof(Index));
            }
            return View(lodging);
        }

        // GET: LodgingController/Edit/5
        public IActionResult Edit(int id)
        {
            var lodging = _lodgingService.GetByID(id);
            return View(lodging);
        }

        // POST: LodgingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Lodging lodging)
        {
            if (ModelState.IsValid)
            {
                _lodgingService.Update(lodging);
                return RedirectToAction(nameof(Index));
            }
            return View(lodging);
        }

        // GET: LodgingController/Delete/5
        public IActionResult Delete(int id)
        {
            var lodging = _lodgingService.GetByID(id);
            return View(lodging);
        }

        // POST: LodgingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Lodging lodging)
        {
            _lodgingService.Delete(lodging);
            return RedirectToAction(nameof(Index));
        }
    }
}
