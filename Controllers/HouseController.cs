using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using midterm2.Model;
using Repository;

namespace Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    
    public class HouseController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly HousesRepository _housesRepository;
        private readonly ClientRepository _clientRepository;

        public HouseController(IConfiguration configuration, HousesRepository housesRepository, ClientRepository clientRepository)
        {
            _configuration = configuration;
            _housesRepository = housesRepository;
            _clientRepository = clientRepository;
        }

        [AllowAnonymous, HttpGet]
        public IActionResult GetAvailableHouses([FromQuery] HouseQuery query)
        {
            var houses = _housesRepository.GetAvailableHouses(query);
            return Ok(houses);
        }
           
        [HttpPost, Authorize]
        public IActionResult CreateHouse([FromBody] House houseDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            var response = _housesRepository.CreateHouse(houseDetails);
            return Ok(response); 
        }

        [HttpPost, Authorize]
        public IActionResult BookHouse([FromBody] BookStay modal)
        {
            var token = TokenManager.GetToken(Request);

            var client = _clientRepository.GetClientByToken(token);
            if (client != null)
            {
                var response = _housesRepository.BookHouse(modal.HouseId, client, modal.CheckInDate, modal.CheckOutDate);
                return Ok(response ? "Booking successful" : "Booking failed");
            }
            else
            {
                return Unauthorized("Client token is not valid");
            }
        }

        [HttpPost, Authorize] 
        public IActionResult CancelBooking(int bookingId)
        {
            bool result = _housesRepository.CancelBooking(bookingId);
            if (result)
            {
                return Ok("Booking cancelled successfully.");
            }
            else
            {
                return NotFound("Booking not found or could not be cancelled.");
            }
        }
    }
}
