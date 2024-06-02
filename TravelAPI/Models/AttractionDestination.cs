namespace TravelAPI.Models
{
    public class AttractionDestination
    {
        public bool status { get; set; }
        public string message { get; set; }
        public long timestamp { get; set; }
        public DataDest data { get; set; }
    }
    public class DataDest
    {
        public Product[] products { get; set; }
        public Destination[] destinations { get; set; }
    }

    public class Product
    {
        public string id { get; set; }
        public string __typename { get; set; }
        public string title { get; set; }
        public string productId { get; set; }
        public string productSlug { get; set; }
        public string taxonomySlug { get; set; }
        public int cityUfi { get; set; }
        public string cityName { get; set; }
        public string countryCode { get; set; }
    }

    public class Destination
    {
        public string id { get; set; }
        public string __typename { get; set; }
        public int ufi { get; set; }
        public string country { get; set; }
        public string cityName { get; set; }
        public int productCount { get; set; }
        public string cc1 { get; set; }
    }

}
