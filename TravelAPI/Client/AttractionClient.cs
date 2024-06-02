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

namespace TravelAPI.Client
{
    public class AttractionClient
    {
        private static string _address;
        private static string _apikey;
        private static string _apihost;

        public AttractionClient()
        {
            _address = Constants.Address;
            _apikey = Constants.ApiKey;
            _apihost = Constants.ApiHost;
        }
        public async Task<AttractionDestination> AttractionGetDest(string quary)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address + $"/attraction/searchLocation?query={quary}&languagecode=uk"),
                Headers =
    {
        { "X-RapidAPI-Key", _apikey },
        { "X-RapidAPI-Host", _apihost },
    },
            };
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AttractionDestination>(body);
            return result;
        }
        public async Task<SearchAttraction> GetAttractions(string id, string arrival, string departure, string currency)
        {
            var client = new HttpClient();
            string url = _address + $"/attraction/searchAttractions?id={id}&";
            if (arrival != "none" && departure != "none")
            {
                url += $"startDate={arrival}&endDate={departure}&";
            }
            url += $"currency_code={currency}&languagecode=uk";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Headers =
    {
        { "X-RapidAPI-Key", _apikey },
        { "X-RapidAPI-Host", _apihost },
    },
            };
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SearchAttraction>(body);
            return result;
        }
        public async Task<AttractionDetails> GetAttractionDetails(string slug, string currency)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address + $"/attraction/getAttractionDetails?slug={slug}&languagecode=uk&currency_code={currency}"),
                Headers =
    {
        { "X-RapidAPI-Key", _apikey },
        { "X-RapidAPI-Host", _apihost },
    },
            };
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AttractionDetails>(body);
            return result;
        }
    }
}
