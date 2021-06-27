using Core.DomainModel;
using DomainServices.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [ApiController]
    [Route("/api/animal")]
    public class AnimalController : Controller
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_animalRepository.GetAllAvailableAnimals());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Animal animal = _animalRepository.GetByID(id);
            return Ok(animal);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Animal animal)
        {
            _animalRepository.Create(animal);
            return Ok(animal);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Animal animal)
        {
            animal.ID = id;
            _animalRepository.Update(animal);
            return Ok(animal);
        }
    }
}