using DomainServices.Repositories;
using Microsoft.AspNetCore.Mvc;
using Core.DomainModel;

namespace WebService.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

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

        [HttpGet]
        [Route("/api/account/{username}")]
        public IActionResult GetByUserName(string username)
        {
            var customer = _userRepository.GetCustomerByUserName(username);
            return Ok(customer);
        }
    }
}