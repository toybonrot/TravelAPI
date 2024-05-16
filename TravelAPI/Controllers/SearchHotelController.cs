using Microsoft.AspNetCore.Mvc;
using TravelAPI.Clients;
using TravelAPI.DataBase;
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
            HotelsBase temp = new HotelsBase();
            temp.InsertHotels(hotel);
            return hotel.data.hotels;
        }
    }
    public class WishListHotel : ControllerBase
    {
        private readonly ILogger<WishListHotel> _logger;

        public WishListHotel(ILogger<WishListHotel> logger)
        {
            _logger = logger;
        }
        //[HttpPost]
        //public Hotel AddHotel()
        //{
        //    HotelClient hotelClient = new HotelClient();
        //}
    }
}
