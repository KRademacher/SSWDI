using Core.DomainModel;
using DomainServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [ApiController]
    [Route("/api/lodging")]
    public class LodgingController : Controller
    {
        private readonly ILodgingService _lodgingService;

        public LodgingController(ILodgingService lodgingService)
        {
            _lodgingService = lodgingService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_lodgingService.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Lodging lodging = _lodgingService.GetByID(id);
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