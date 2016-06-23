using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using DAL.Interfaces;
using System.Configuration;
using System.Data.SqlClient;
using log4net;

namespace DAL.DataBase
{
   public class AlbumsDAO: IAlbumsDAO
    {
        readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _connectionString;

        public AlbumsDAO()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public IEnumerable<Album> GetAll()
        {
            List<Album> albums = new List<Album>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT [Id], [Login], [Title] FROM dbo.[Albums]", connection);
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        albums.Add(new Album((int)reader["Id"], (string)reader["Login"], (string)reader["Title"]));
                    }
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при взятии всех альбомов из базы данных!");
            }

            foreach (var item in albums)
            {
                yield return item;
            }

        }

        public int AddAlbum(string login, string title)
        {

            int albumId = -1;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var commandInsert = new SqlCommand("INSERT INTO dbo.[Albums] ([Login], [Title]) VALUES (@Login, @Title) ", connection);
                    commandInsert.Parameters.AddWithValue("@Login", login);
                    commandInsert.Parameters.AddWithValue("@Title", title);
                    connection.Open();
                    var result = commandInsert.ExecuteNonQuery();

                }

                using (var connection = new SqlConnection(_connectionString))//ищем max id альбома, у которого Login и Title равны созданному
                {
                    var command = new SqlCommand("SELECT [Id] FROM dbo.[Albums] WHERE [Login] = @Login AND [Title] = @Title", connection);
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Title", title);
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (albumId < (int)reader["Id"])
                            albumId = (int)reader["Id"];
                    }

                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при добавлении альбома в базу данных!");
            }


            return albumId;
        }

        public void DeleteAlbum(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("DELETE FROM dbo.[Albums] WHERE [Id] = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();

                    var result = command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при удалении альбома из базы данных!");
            }

        }
        public Album GetAlbum(int albumId)
        {
            Album ent = null;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT [Id], [Login], [Title] FROM dbo.[Albums] WHERE [Id] = @Id", connection);
                    command.Parameters.AddWithValue("@Id", albumId);
                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (!reader.Read()) return null;

                    ent = new Album((int)reader["Id"], (string)reader["Login"], (string)reader["Title"]);
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при получении альбома из базы данных!");
            }

            return ent;
        }
    }
}
