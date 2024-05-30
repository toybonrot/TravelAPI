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
        public SearchHotel GetHotelInfo(string query, string arrival, string departure, string user, string filters, double priceMax, int pageNum)
        {
            HotelClient hotelClient = new HotelClient();
            SearchDestination destination = hotelClient.GetDestination(query).Result;
            SearchHotel hotel = hotelClient.GetHotel(destination.data[0].dest_id, arrival, departure, filters, priceMax, pageNum).Result;
            HotelsBase temp = new HotelsBase();
            temp.InsertHotels(hotel, query, user);
            return hotel;
        }
        [HttpGet]
        [Route("Read Available Filters Controller")]
        public List<string> ReadFilters()
        {
            HotelsBase temp = new HotelsBase();
            return temp.GetAvailableFilters().Result;
        }
        [HttpGet]
        [Route("Get Filter Controller")]
        public string GetFilter(string filter)
        {
            HotelsBase temp = new HotelsBase();
            return temp.GetFilterId(filter).Result;
        }
        [HttpPost]
        [Route("Add Hotel Controller")]
        public BaseHotel AddHotel(int id, string user)
        {
            HotelsBase temp = new HotelsBase();
            return temp.InsertIntoWishList(id, user).Result;
        }
        [HttpGet]
        [Route("Read Hotel List Controller")]
        public List<BaseHotel> ReadHotel(string user)
        {
            HotelsBase temp = new HotelsBase();
            return temp.GetFromWishList(user).Result;
        }
        [HttpDelete]
        [Route("Delete Hotel Controller")]
        public Task DeleteHotel(int id, string user)
        {
            HotelsBase temp = new HotelsBase();
            return temp.DeleteFromWishList(id, user); 
        }
    }
}
