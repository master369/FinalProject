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
   public class PhotosDAO: IPhotosDAO
    {

        readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _connectionString;

        public PhotosDAO()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public IEnumerable<Photo> GetAllPhotos()
        {
            List<Photo> photos = new List<Photo>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT [Id], [AlbumId], [Original], [Small], [Date], [AccountLogin], [Title] FROM dbo.[Photos]", connection);
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        photos.Add(new Photo((int)reader["Id"], (int)reader["AlbumId"], (string)reader["Title"],
                                            (string)reader["AccountLogin"], (byte[])reader["Original"]));
                    }
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при получении всех фотографий из базы данных!");
            }


            foreach (var item in photos)
            {
                item.LikesContainer = GetLikesForPhoto(item.Id);
                yield return item;
            }
        }

        public IEnumerable<Photo> GetAllPhotosByAccount(string login)
        {
            List<Photo> photos = new List<Photo>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT [Id], [AlbumId], [Original], [Small], [Date], [AccountLogin], [Title] FROM dbo.[Photos] WHERE [AccountLogin] = @Login", connection);
                    command.Parameters.AddWithValue("@Login", login);

                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        photos.Add(new Photo((int)reader["Id"], (int)reader["AlbumId"], (string)reader["Title"],
                                            (string)reader["AccountLogin"], (byte[])reader["Original"]));
                    }
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при получении всех фотографий из базы данных для аккаунта!");
            }


            foreach (var item in photos)
            {
                item.LikesContainer = GetLikesForPhoto(item.Id);
                yield return item;
            }
        }

        public Photo GetPhoto(int id)
        {
            Photo ent = null;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT [Id], [AlbumId], [Original], [Small], [Date], [AccountLogin], [Title] FROM dbo.[Photos] WHERE [Id] = @Id", connection);
                    command.Parameters.Add("@Id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (!reader.Read()) return null;

                    ent = new Photo((int)reader["Id"], (int)reader["AlbumId"], (string)reader["Title"],
                                            (string)reader["AccountLogin"], (byte[])reader["Original"]);
                    ent.LikesContainer = GetLikesForPhoto(ent.Id);
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при получении фотографии из базы данных!");
            }

            return ent;
        }

        public void DeletePhoto(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("DELETE FROM dbo.[Photos] WHERE [Id] = @Id", connection);
                    command.Parameters.Add("@Id", id);
                    connection.Open();

                    var result = command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при удалении фотографии из базы данных!");
            }

        }

        public void AddPhoto(int albumId, string title, string accountLogin, byte[] image)
        {
            var ent = new Photo(0, albumId, title, accountLogin, image);
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var commandInsert = new SqlCommand("INSERT INTO dbo.[Photos] ([Original], [Small], [Date], [AccountLogin], [Title], [AlbumId])" +
                                                        " VALUES (@Original, @Small, @Date, @AccountLogin, @Title, @AlbumId) ", connection);
                    commandInsert.Parameters.AddWithValue("@Original", ent.OriginalImage);
                    commandInsert.Parameters.AddWithValue("@Small", ent.SmallImage);
                    commandInsert.Parameters.AddWithValue("@Date", ent.AddDate);
                    commandInsert.Parameters.AddWithValue("@AccountLogin", ent.AccountLogin);
                    commandInsert.Parameters.AddWithValue("@Title", ent.Title);
                    commandInsert.Parameters.AddWithValue("@AlbumId", ent.AlbumId);
                    connection.Open();
                    var result = commandInsert.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при добавлении фотографии в базу данных!");
            }
        }

        public bool LikePhoto(int photoId, string login)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT [PhotoId], [Login] FROM dbo.[Likes] WHERE [PhotoId] = @Id And [Login] = @Login", connection);
                    command.Parameters.AddWithValue("@Id", photoId);
                    command.Parameters.AddWithValue("@Login", login);
                    connection.Open();
                    var reader = command.ExecuteReader();


                    if (!reader.Read())
                    {
                        connection.Close();
                        connection.Open();

                        command = new SqlCommand("INSERT INTO dbo.[Likes] ([PhotoId], [Login]) VALUES  (@PhotoId, @Login)", connection);

                        command.Parameters.AddWithValue("@PhotoId", photoId);
                        command.Parameters.AddWithValue("@Login", login);
                        command.ExecuteNonQuery();

                        return true;
                    }

                    connection.Close();
                    connection.Open();

                    command = new SqlCommand("DELETE FROM dbo.[Likes] WHERE [PhotoId] = @PhotoId And [Login] = @Login", connection);

                    command.Parameters.AddWithValue("@PhotoId", photoId);
                    command.Parameters.AddWithValue("@Login", login);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при добавлении лайка к фотографии в базу данных!");
            }

            return false;
        }


        public List<string> GetLikesForPhoto(int id)
        {
            List<string> likesContainer = new List<string>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT [PhotoId], [Login] FROM dbo.[Likes] WHERE [PhotoId] = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        likesContainer.Add((string)reader["Login"]);
                    }
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при получении лайков к фотографии из базы данных!");
            }



            return likesContainer;
        }


    }
}
