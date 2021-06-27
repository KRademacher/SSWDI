using Core.DomainModel;
using DomainServices.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebService.Controllers
{
    [ApiController]
    [Route("/api/lodging")]
    public class LodgingController : Controller
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ILodgingRepository _lodgingRepository;

        public LodgingController(IAnimalRepository animalRepository, ILodgingRepository lodgingRepository)
        {
            _animalRepository = animalRepository;
            _lodgingRepository = lodgingRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lodgings = _lodgingRepository.GetAll();
            foreach (var lodging in lodgings)
            {
                var lodgingAnimals = _animalRepository.GetAll().Where(a => a.LodgingID == lodging.ID);
                lodging.LodgingAnimals.AddRange(lodgingAnimals);
            }
            return Ok(lodgings);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Lodging lodging = _lodgingRepository.GetByID(id);
            var lodgingAnimals = _animalRepository.GetAll().Where(a => a.LodgingID == id);
            lodging.LodgingAnimals.AddRange(lodgingAnimals);
            return Ok(lodging);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id)
        {
            Lodging lodging = _lodgingRepository.GetByID(id);
            _lodgingRepository.Update(lodging);
            return Ok(lodging);
        }
    }
}