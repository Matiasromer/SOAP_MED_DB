using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SOAP_MED_DB
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private const string ConnectionString =
                "Server=tcp:eventmserver.database.windows.net,1433;Initial Catalog=EMDatabase;Persist Security Info=False;User ID=Matias;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
            ;
        public int AddGame(string titel, double rating)
        {
            const string sql = "insert into Games (Titel, Rating) values (@Titel, @Rating)";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand insertCommand = new SqlCommand(sql, conn))
                {
                    insertCommand.Parameters.AddWithValue("@Titel", titel);
                    insertCommand.Parameters.AddWithValue("@Rating", rating);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        public Game GetOneGame(string id)
        {
            Game spil = new Game();
            int idint = Convert.ToInt32(id);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = $"SELECT * FROM Games WHERE ID = '{id}'";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@id", idint);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    spil.Id = reader.GetInt32(0);
                    spil.Titel = reader.GetString(1);
                    spil.Rating = reader.GetDouble(2);

                }
            }
            return spil;
        }

        public void DeleteGame(int id)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            conn.Open();

            cmd.CommandText = @"DELETE FROM Games WHERE Games.id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<Game> GetGames()
        {
            const string sql = "select * from Games order by id";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(sql, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())

                    {
                        List<Game> gameList = new List<Game>();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string titel = reader.GetString(1);
                            double rating = reader.GetDouble(2);
                            Game g1 = new Game()
                            {
                                Id = id,
                                Titel = titel,
                                Rating = rating
                            };
                            gameList.Add(g1);
                        }
                        return gameList;
                    }
                }
            }
        }

        public void UpdateGame(int id, Game spil)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand();

            command.Connection = conn;
            conn.Open();

            command.CommandText = @"UPDATE Games 
                                SET Titel = @titel, 
                                    Rating = @rating, 
                                WHERE Games.Id = @id";

            command.Parameters.AddWithValue("@id", spil.Id);
            command.Parameters.AddWithValue("@titel", spil.Titel);
            command.Parameters.AddWithValue("@rating", spil.Rating);


            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}
