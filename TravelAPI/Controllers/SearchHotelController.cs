using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using TravelAPI.Clients;
using TravelAPI.DataBase;
using TravelAPI.Models;

namespace TravelAPI.Controllers
{
    [ApiController]
    public class SearchHotelController : ControllerBase
    {
        private readonly ILogger<SearchHotelController> _logger;

        public SearchHotelController(ILogger<SearchHotelController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Route("Search Hotel Controller")]
        public Hotel[] GetHotelInfo(string query, string arrival, string departure)
        {
            HotelClient hotelClient = new HotelClient();
            SearchDestination destination = hotelClient.GetDestination(query).Result;
            SearchHotel hotel = hotelClient.GetHotel(destination.data[0].dest_id, arrival, departure).Result;
            HotelsBase temp = new HotelsBase();
            temp.InsertHotels(hotel);
            return hotel.data.hotels;
        }   
        [HttpPost]
        [Route("Add Hotel Controller")]
        public BaseHotel AddHotel(int id)
        {
            HotelsBase temp = new HotelsBase();
            return temp.InsertIntoWishList(id).Result;
        }
        [HttpDelete]
        [Route("Delete Hotel Controller")]
        public Task DeleteHotel(int id)
        {
            HotelsBase temp = new HotelsBase();
            return temp.DeleteFromWishList(id); 
        }
    }
}
