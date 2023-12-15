using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    
    public class ClientController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ClientRepository _clientRepository;

        public ClientController(IConfiguration configuration, ClientRepository clientRepository)
        {
            _configuration = configuration;
            _clientRepository = clientRepository; 
        }

        [HttpPost]
        public IActionResult Login([FromBody] Login modal)
        {
            var client = _clientRepository.GetClientLogin(modal);
            if (client != null)
            {
                return Ok(new { Token = client.Token });
            }
            return Unauthorized(); // Return unauthorized if login fails
        }

        [HttpPost]
        public IActionResult SignUp([FromBody] SignUp modal)
        {
            var newClient = _clientRepository.CreateClient(modal);
            if (newClient != null)
            {
                return Ok(newClient);
            }
            return BadRequest("Client creation failed"); // Return an error message if sign-up fails
        }
    }
}
