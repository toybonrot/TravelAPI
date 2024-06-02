using Npgsql;
using TravelAPI.Models;

namespace TravelAPI.DataBase
{
    public class AttractionBase
    {
        NpgsqlConnection con = new NpgsqlConnection(Constants.Connection);
        public async Task InsertAttraction(SearchAttraction attraction, string user)
        {
            var sql = "insert into public.\"AttractionHistory\" (\"User\", \"City\", \"Attraction_Id\", \"Name\", \"Slug\", " +
                "\"Description\", \"Price\", \"Currency\", \"PhotoURL\", \"Score\") " +
             $"values (@User1, @City1, @Attraction_Id1, @Name1, @Slug1, @Description1, @Price1, @Currency1, @PhotoURL1, @Score1)";
            for (int i = 0; i < attraction.data.products.Length; i++)
            {
                await con.OpenAsync();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                double score = attraction.data.products[i].reviewsStats != null ? attraction.data.products[i].reviewsStats.combinednumericstats.average : 0;
                cmd.Parameters.AddWithValue("User1", user);
                cmd.Parameters.AddWithValue("City1", attraction.data.products[i].ufiDetails.bCityName);
                cmd.Parameters.AddWithValue("Attraction_Id1", attraction.data.products[i].id);
                cmd.Parameters.AddWithValue("Name1", attraction.data.products[i].name);
                cmd.Parameters.AddWithValue("Slug1", attraction.data.products[i].slug);
                cmd.Parameters.AddWithValue("Description1", attraction.data.products[i].shortDescription);
                cmd.Parameters.AddWithValue("Price1", attraction.data.products[i].representativePrice.publicAmount);
                cmd.Parameters.AddWithValue("Currency1", attraction.data.products[i].representativePrice.currency);
                cmd.Parameters.AddWithValue("PhotoURL1", attraction.data.products[i].primaryPhoto.small);
                cmd.Parameters.AddWithValue("Score1", score);
                await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();
            }
        }
        public async Task<BaseAttraction> InsertAttractionWishList(string id, string user)
        {
            var sql = $"SELECT * FROM public.\"AttractionHistory\" where \"Attraction_Id\" = '{id}' and \"User\" = '{user}' ";
            await con.OpenAsync();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            await reader.ReadAsync();
            BaseAttraction attraction = new BaseAttraction()
            {
                user = user,
                city = reader.GetString(2),
                id = reader.GetString(3),
                name = reader.GetString(4),
                slug = reader.GetString(5),
                description = reader.GetString(6),
                price = reader.GetDouble(7),
                currency = reader.GetString(8),
                photoURL = reader.GetString(9),
                score = reader.GetDouble(10)

            };
            await con.CloseAsync();
            sql = "insert into public.\"AttractionList\" (\"User\", \"City\", \"Attraction_Id\", \"Name\", \"Slug\", " +
                 "\"Description\", \"Price\", \"Currency\", \"PhotoURL\", \"Score\")" +
             $"values (@User1, @City1, @Attraction_Id1, @Name1, @Slug1, @Description1, @Price1, @Currency1, @PhotoURL1, @Score1)";
            await con.OpenAsync();
            NpgsqlCommand cmd2 = new NpgsqlCommand(sql, con);
            cmd2.Parameters.AddWithValue("User1", user);
            cmd2.Parameters.AddWithValue("City1", attraction.city);
            cmd2.Parameters.AddWithValue("Attraction_Id1", attraction.id);
            cmd2.Parameters.AddWithValue("Name1", attraction.name);
            cmd2.Parameters.AddWithValue("Slug1", attraction.slug);
            cmd2.Parameters.AddWithValue("Description1", attraction.description);
            cmd2.Parameters.AddWithValue("Price1", attraction.price);
            cmd2.Parameters.AddWithValue("Currency1", attraction.currency);
            cmd2.Parameters.AddWithValue("PhotoURL1", attraction.photoURL);
            cmd2.Parameters.AddWithValue("Score1", attraction.score);
            cmd2.ExecuteNonQuery();
            await con.CloseAsync();
            return attraction;
        }
        public async Task<List<BaseAttraction>> GetFromAttractionWishList(string user)
        {
            var sql = $"SELECT * FROM public.\"AttractionList\" where \"User\" = '{user}'";
            await con.OpenAsync();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<BaseAttraction> attractionList = new List<BaseAttraction>();
            while (await reader.ReadAsync())
            {
                BaseAttraction attraction = new BaseAttraction()
                {
                    user = reader.GetString(1),
                    city = reader.GetString(2),
                    id = reader.GetString(3),
                    name = reader.GetString(4),
                    slug = reader.GetString(5),
                    description = reader.GetString(6),
                    price = reader.GetDouble(7),
                    currency = reader.GetString(8),
                    photoURL = reader.GetString(9),
                    score = reader.GetDouble(10)
                };
                attractionList.Add(attraction);
            }
            await con.CloseAsync();
            return attractionList;
        }
        public async Task DeleteFromAttractionWishList(string user, string id)
        {
            var sql = $"DELETE FROM \"AttractionList\" where \"Attraction_Id\" = '{id}' and \"User\" = '{user}'";
            await con.OpenAsync();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
        }
    }
}

