using Core.DomainModel;
using DomainServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [ApiController]
    [Route("/api/animal")]
    public class AnimalController : Controller
    {
        private readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_animalService.GetAllAvailableAnimals());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Animal animal = _animalService.GetByID(id);
            return Ok(animal);
        }

        [Authorize(Policy = "RequireVolunteerOrCustomer")]
        [HttpPost]
        public IActionResult Create([FromBody] Animal animal)
        {
            _animalService.Create(animal);
            return Ok(animal);
        }

        [Authorize(Policy = "RequireVolunteerOrCustomer")]
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Animal animal)
        {
            animal.ID = id;
            _animalService.Update(animal);
            return Ok(animal);
        }
    }
}