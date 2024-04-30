using Microsoft.AspNetCore.Mvc;
using TravelAPI.Clients;
using TravelAPI.Models;

namespace TravelAPI.Controllers
{
    [ApiController]
    [Route("Hotel Controller")]
    public class SearchHotelController : ControllerBase
    {
        private readonly ILogger<SearchHotelController> _logger;

        public SearchHotelController(ILogger<SearchHotelController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public Hotel[] GetHotelInfo(string quary, string arrival, string departure)
        {
            HotelClient hotelClient = new HotelClient();
            SearchDestination destination = hotelClient.GetDestination(quary).Result;
            SearchHotel hotel = hotelClient.GetHotel(destination.data[0].dest_id, arrival, departure).Result;
            return hotel.data.hotels;
        }
    }
}
