using Npgsql;
using TravelAPI.Models;

namespace TravelAPI.DataBase
{
    public class HotelsBase
    {
        NpgsqlConnection con = new NpgsqlConnection(Constants.Connection);

        public async Task InsertHotels(SearchHotel hotels)
        {
            var sql = "insert into public.\"GetHotels\" (\"Name\", \"Score\", \"Reviews\", \"Hotel_id\", " +
             "\"Price\", \"Currency\") " +
             $"values (@Name1, @Score1, @Reviews1, @Hotel_id1, @Price1, @Currency1)";          
            for (int i = 0; i < hotels.data.hotels.Length; i++)
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("Name1", hotels.data.hotels[i].property.name);
                cmd.Parameters.AddWithValue("Score1", hotels.data.hotels[i].property.reviewScore);
                cmd.Parameters.AddWithValue("Reviews1", hotels.data.hotels[i].property.reviewCount);
                cmd.Parameters.AddWithValue("Hotel_id1", hotels.data.hotels[i].property.id);
                cmd.Parameters.AddWithValue("Price1", hotels.data.hotels[i].property.priceBreakdown.grossPrice.value);
                cmd.Parameters.AddWithValue("Currency1", hotels.data.hotels[i].property.priceBreakdown.grossPrice.currency);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
