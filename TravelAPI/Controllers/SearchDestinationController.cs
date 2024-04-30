using Microsoft.AspNetCore.Mvc;
using TravelAPI.Clients;
using TravelAPI.Models;

namespace TravelAPI.Controllers
{
    [ApiController]
    [Route("Destination Controller")]
    public class SearchDestinationController : ControllerBase
    {
        private readonly ILogger<SearchDestinationController> _logger;

        public SearchDestinationController(ILogger<SearchDestinationController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public string GetInfoDestination(string quary)
        {
            HotelClient hotelClient = new HotelClient();
            SearchDestination destination = hotelClient.GetDestination(quary).Result;
            return $"{destination.data[0].country} \n {destination.data[0].city_name}";
        }
    }
}
