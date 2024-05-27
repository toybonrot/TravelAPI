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
        public async Task<BaseHotel> InsertIntoWishList(int query)
        {
            var sql = $"SELECT * FROM public.\"GetHotels\" where \"Hotel_id\" = {query}";
            await con.OpenAsync();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            await reader.ReadAsync();
            BaseHotel hotel = new BaseHotel() {Name = reader.GetString(1), Score = reader.GetDouble(2), Reviews = reader.GetInt32(3), Hotel_id = reader.GetInt32(4), Price = reader.GetDouble(5), Currency = reader.GetString(6) };
            await con.CloseAsync();
            await con.OpenAsync();
            sql = "insert into public.\"WishList\" (\"Name\", \"Score\", \"Reviews\", \"Hotel_id\", " +
             "\"Price\", \"Currency\") " +
             $"values (@Name1, @Score1, @Reviews1, @Hotel_id1, @Price1, @Currency1)";
            NpgsqlCommand cmd2 = new NpgsqlCommand(sql, con);
            cmd2.Parameters.AddWithValue("Name1", hotel.Name);
            cmd2.Parameters.AddWithValue("Score1", hotel.Score);
            cmd2.Parameters.AddWithValue("Reviews1", hotel.Reviews);
            cmd2.Parameters.AddWithValue("Hotel_id1", hotel.Hotel_id);
            cmd2.Parameters.AddWithValue("Price1", hotel.Price);
            cmd2.Parameters.AddWithValue("Currency1", hotel.Currency);
            await cmd2.ExecuteNonQueryAsync();
            await con.CloseAsync();
            return hotel;
        }
        public async Task DeleteFromWishList(int query)
        {
            var sql = $"DELETE FROM \"WishList\" where \"Hotel_id\" = {query}";
            await con.OpenAsync();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
        }
    }
}
