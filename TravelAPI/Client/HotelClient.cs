using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravelAPI.Models;
using TravelAPI;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace TravelAPI.Clients
{
    public class HotelClient
    {
        private static string _address;
        private static string _apikey;
        private static string _apihost;

        public HotelClient()
        {
            _address = Constants.Address;
            _apikey = Constants.ApiKey;
            _apihost = Constants.ApiHost;
        }
        public async Task<SearchDestination> GetDestination(string quary)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address + $"/hotels/searchDestination?query={quary}"),
                Headers =
    {
        { "X-RapidAPI-Key", _apikey },
        { "X-RapidAPI-Host", _apihost },
    },
            };
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(body);

            var result = JsonConvert.DeserializeObject<SearchDestination>(body);
            return result;
        }
        public async Task<SearchHotel> GetHotel(string id, string arrival, string departure)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address + $"/hotels/searchHotels?dest_id={id}&search_type=CITY&arrival_date={arrival}&departure_date={departure}&adults=1&children_age=0%2C17&room_qty=1&page_number=1&languagecode=en-us&currency_code=UAH"),
                Headers =
    {
        { "X-RapidAPI-Key", _apikey },
        { "X-RapidAPI-Host", _apihost },
    },
            };
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<SearchHotel>(body);
            return result;

        }
    }

}
