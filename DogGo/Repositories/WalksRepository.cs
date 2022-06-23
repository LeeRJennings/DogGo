using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly IConfiguration _config;

        public WalksRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walks> GetWalksByWalker(int walkerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT w.Id, w.Date, w.Duration, w.WalkerId, w.DogId, d.Name AS Dog, o.Name AS Owner
                                        FROM Walks w
                                        JOIN Walker wa ON wa.Id = w.WalkerId
                                        JOIN Dog d ON d.Id = w.DogId
                                        JOIN Owner o ON o.Id = d.OwnerId
                                        WHERE w.WalkerId = @walkerId";
                    cmd.Parameters.AddWithValue("@walkerId", walkerId);
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Walks> walks = new List<Walks>();
                        while (reader.Read())
                        {
                            Walks walk = new Walks
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                                DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Dog = new Dog
                                {
                                    Name = reader.GetString(reader.GetOrdinal("Dog"))
                                },
                                Owner = new Owner
                                {
                                    Name = reader.GetString(reader.GetOrdinal("Owner"))
                                }
                            };

                            walks.Add(walk);
                        }
                        return walks;
                    }
                }
            }
        }
    }
}
