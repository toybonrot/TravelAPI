using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Extensions.Primitives;
using TravelAPI.Client;
using TravelAPI.DataBase;
using TravelAPI.Models;

namespace TravelAPI.Controllers
{
    [ApiController]
    [Route("Attraction Controller")]
    public class SearchAttractionController : ControllerBase
    {
        private readonly ILogger<SearchAttractionController> _logger;

        public SearchAttractionController(ILogger<SearchAttractionController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Route("Search Attraction")]
        public SearchAttraction GetAttractionInfo(string query, string arrival, string departure, string currency, string user)
        {
            AttractionClient attractionClient = new AttractionClient();
            AttractionDestination destination = attractionClient.AttractionGetDest(query).Result;
            SearchAttraction attraction = attractionClient.GetAttractions(destination.data.destinations[0].id, arrival, departure, currency).Result;
            AttractionBase temp = new AttractionBase();
            temp.InsertAttraction(attraction, user);
            return attraction;

        }
        [HttpGet]
        [Route("Get Attraction Details")]
        public AttractionDetails GetAttractionDetails(string slug, string currency)
        {
            AttractionClient attractionClient = new AttractionClient();
            AttractionDetails attraction = attractionClient.GetAttractionDetails(slug, currency).Result;
            return attraction;
        }
        [HttpPost]
        [Route("Add Attraction")]
        public BaseAttraction AddAttraction(string id, string user)
        {
            AttractionBase temp = new AttractionBase();
            return temp.InsertAttractionWishList(id, user).Result;
        }
        [HttpGet]
        [Route("Get Attraction List")]
        public List<BaseAttraction> GetAttractionsFromList(string user)
        {
            AttractionBase temp = new AttractionBase();
            return temp.GetFromAttractionWishList(user).Result;
        }
        [HttpDelete]
        [Route("Delete Attraction")]
        public Task DeleteAttraction(string id, string user)
        {
            AttractionBase temp = new AttractionBase();
            return temp.DeleteFromAttractionWishList(user, id);
        }
    }
}
