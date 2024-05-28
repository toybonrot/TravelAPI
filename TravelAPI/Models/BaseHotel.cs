namespace TravelAPI.Models
{
    public class BaseHotel
    {
        public string User { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public double Score { get; set; }
        public int Reviews { get; set; }
        public int Hotel_id { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public string PhotoURL { get; set; }
    }
}
