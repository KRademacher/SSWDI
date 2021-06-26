using DomainServices.Repositories;
using Microsoft.AspNetCore.Mvc;
using Core.DomainModel;

namespace WebService.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("/api/register")]
        public IActionResult Create([FromBody] Customer customer)
        {
            _userRepository.Create(customer);
            return Ok(customer);
        }
    }
}