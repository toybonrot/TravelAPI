namespace TravelAPI.Models
{
    public class BaseHotel
    {
        public string Name { get; set; }
        public double Score { get; set; }
        public int Reviews { get; set; }
        public int Hotel_id { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
    }
}
