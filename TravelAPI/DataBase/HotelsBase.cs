using Npgsql;
using TravelAPI.Models;

namespace TravelAPI.DataBase
{
    public class HotelsBase
    {
        NpgsqlConnection con = new NpgsqlConnection(Constants.Connection);

        public async Task InsertHotels(SearchHotel hotels, string city, string user)
        {
            var sql = "insert into public.\"HotelHistory2\" (\"User\", \"City\", \"Name\", \"Stars\", \"Score\", \"Reviews\"," +
                " \"Hotel_Id\", \"Price\", \"Currency\", \"PhotoURL\") " +
             $"values (@User1, @City1, @Name1, @Stars1, @Score1, @Reviews1, @Hotel_Id1, @Price1, @Currency1, @PhotoURL1)";          
            for (int i = 0; i < hotels.data.hotels.Length; i++)
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("User1", user);
                cmd.Parameters.AddWithValue("City1", city);
                cmd.Parameters.AddWithValue("Name1", hotels.data.hotels[i].property.name);
                cmd.Parameters.AddWithValue("Stars1", hotels.data.hotels[i].property.propertyClass);
                cmd.Parameters.AddWithValue("Score1", hotels.data.hotels[i].property.reviewScore);
                cmd.Parameters.AddWithValue("Reviews1", hotels.data.hotels[i].property.reviewCount);
                cmd.Parameters.AddWithValue("Hotel_Id1", hotels.data.hotels[i].property.id);
                cmd.Parameters.AddWithValue("Price1", hotels.data.hotels[i].property.priceBreakdown.grossPrice.value);
                cmd.Parameters.AddWithValue("Currency1", hotels.data.hotels[i].property.priceBreakdown.grossPrice.currency);
                cmd.Parameters.AddWithValue("PhotoURL1", hotels.data.hotels[i].property.photoUrls[0]);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public async Task<BaseHotel> InsertIntoWishList(int query, string user)
        {
            var sql = $"SELECT * FROM public.\"HotelHistory2\" where \"Hotel_Id\" = {query} and \"User\" = '{user}' ";
            await con.OpenAsync();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            await reader.ReadAsync();
            BaseHotel hotel = new BaseHotel() 
            {
                User = user,
                City = reader.GetString(1), 
                Name = reader.GetString(2), 
                Stars = reader.GetInt32(3), 
                Score = reader.GetDouble(4), 
                Reviews = reader.GetInt32(5), 
                Hotel_id = reader.GetInt32(6), 
                Price = reader.GetDouble(7), 
                Currency = reader.GetString(8), 
                PhotoURL = reader.GetString(9)
            };
            await con.CloseAsync();
            await con.OpenAsync();
            sql = "insert into public.\"HotelList2\"(\"User\", \"City\", \"Name\", \"Stars\", \"Score\", \"Reviews\"," +
                " \"Hotel_Id\", \"Price\", \"Currency\", \"PhotoURL\") " +
             $"values (@User1, @City1, @Name1, @Stars1, @Score1, @Reviews1, @Hotel_Id1, @Price1, @Currency1, @PhotoURL1)";
            NpgsqlCommand cmd2 = new NpgsqlCommand(sql, con);
            cmd2.Parameters.AddWithValue("User1", hotel.User);
            cmd2.Parameters.AddWithValue("City1", hotel.City);
            cmd2.Parameters.AddWithValue("Name1", hotel.Name);
            cmd2.Parameters.AddWithValue("Stars1", hotel.Stars);
            cmd2.Parameters.AddWithValue("Score1", hotel.Score);
            cmd2.Parameters.AddWithValue("Reviews1", hotel.Reviews);
            cmd2.Parameters.AddWithValue("Hotel_id1", hotel.Hotel_id);
            cmd2.Parameters.AddWithValue("Price1", hotel.Price);
            cmd2.Parameters.AddWithValue("Currency1", hotel.Currency);
            cmd2.Parameters.AddWithValue("PhotoURL1", hotel.PhotoURL);
            await cmd2.ExecuteNonQueryAsync();
            await con.CloseAsync();
            return hotel;
        }
        public async Task<List<BaseHotel>> GetFromWishList(string user)
        {
            var sql = $"SELECT * FROM public.\"HotelList2\" where \"User\" = '{user}'";
            await con.OpenAsync();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<BaseHotel> hotelsList = new List<BaseHotel>();
            while (await reader.ReadAsync())
            {
                BaseHotel hotel = new BaseHotel()
                {
                    User = reader.GetString(0),
                    City = reader.GetString(1),
                    Name = reader.GetString(2),
                    Stars = reader.GetInt32(3),
                    Score = reader.GetDouble(4),
                    Reviews = reader.GetInt32(5),
                    Hotel_id = reader.GetInt32(6),
                    Price = reader.GetDouble(7),
                    Currency = reader.GetString(8),
                    PhotoURL = reader.GetString(9)
                };
                hotelsList.Add(hotel);
            }         
            await con.CloseAsync();
            return hotelsList;
        }
        public async Task DeleteFromWishList(int query, string user)
        {
            var sql = $"DELETE FROM \"HotelList2\" where \"Hotel_Id\" = {query} and \"User\" = '{user}'";
            await con.OpenAsync();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
        }
    }
}
