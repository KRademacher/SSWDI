using Core.DomainModel;
using DomainServices.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebService.Controllers
{
    [ApiController]
    [Route("/api/lodging")]
    public class LodgingController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly ILodgingService _lodgingService;

        public LodgingController(IAnimalService animalService, ILodgingService lodgingService)
        {
            _animalService = animalService;
            _lodgingService = lodgingService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lodgings = _lodgingService.GetAll();
            foreach (var lodging in lodgings)
            {
                var lodgingAnimals = _animalService.GetAll().Where(a => a.LodgingID == lodging.ID);
                lodging.LodgingAnimals.AddRange(lodgingAnimals);
            }
            return Ok(lodgings);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Lodging lodging = _lodgingService.GetByID(id);
            var lodgingAnimals = _animalService.GetAll().Where(a => a.LodgingID == id);
            lodging.LodgingAnimals.AddRange(lodgingAnimals);
            return Ok(lodging);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id)
        {
            Lodging lodging = _lodgingService.GetByID(id);
            _lodgingService.Update(lodging);
            return Ok(lodging);
        }
    }
}