using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using TravelAPI.Client;
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
        [Route("Search Hotel")]
        public SearchHotel GetHotelInfo(string query, string arrival, string departure, string user, string filters, double priceMax, int pageNum, string currency)
        {
            HotelClient hotelClient = new HotelClient();
            SearchDestination destination = hotelClient.GetDestination(query).Result;
            SearchHotel hotel = hotelClient.GetHotel(destination.data[0].dest_id, arrival, departure, filters, priceMax, pageNum, currency).Result;
            HotelsBase temp = new HotelsBase();
            temp.InsertHotels(hotel, query, user);
            return hotel;
        }
        [HttpGet]
        [Route("Get More Hotel Details")]
        public HotelInfo GetHotelDetails(int id, string arrival, string departure, string currency)
        {
            HotelClient hotelClient = new HotelClient();
            HotelInfo hotel = hotelClient.GetMoreHotelDetail(id, arrival, departure, currency).Result;
            return hotel;
        }
        [HttpGet]
        [Route("Read Available Filters")]
        public List<string> ReadFilters()
        {
            HotelsBase temp = new HotelsBase();
            return temp.GetAvailableFilters().Result;
        }
        [HttpGet]
        [Route("Get Filter")]
        public string GetFilter(string filter)
        {
            HotelsBase temp = new HotelsBase();
            return temp.GetFilterId(filter).Result;
        }
        [HttpPost]
        [Route("Add Hotel")]
        public BaseHotel AddHotel(int id, string user)
        {
            HotelsBase temp = new HotelsBase();
            return temp.InsertIntoWishList(id, user).Result;
        }
        [HttpGet]
        [Route("Read Hotel List")]
        public List<BaseHotel> ReadHotel(string user)
        {
            HotelsBase temp = new HotelsBase();
            return temp.GetFromWishList(user).Result;
        }
        [HttpDelete]
        [Route("Delete Hotel")]
        public Task DeleteHotel(int id, string user)
        {
            HotelsBase temp = new HotelsBase();
            return temp.DeleteFromWishList(id, user); 
        }
    }
}
