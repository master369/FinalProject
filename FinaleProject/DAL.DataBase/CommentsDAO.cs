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
   public class CommentsDAO: ICommentsDAO
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _connectionString;

        public CommentsDAO()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }


        public void AddComment(string accountLogin, int photoId, string text)
        {
            Comment ent = new Comment(accountLogin, photoId, text);
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var commandInsert = new SqlCommand("INSERT INTO dbo.[Comments] ([AccountLogin], [PhotoId], [Date], [Text])" +
                                                       " VALUES (@AccountLogin, @PhotoId, @Date, @Text) ", connection);
                    commandInsert.Parameters.AddWithValue("@AccountLogin", ent.AccountLogin);
                    commandInsert.Parameters.AddWithValue("@PhotoId", ent.PhotoId);
                    commandInsert.Parameters.AddWithValue("@Date", ent.Date);
                    commandInsert.Parameters.AddWithValue("@Text", ent.Text);
                    connection.Open();
                    var result = commandInsert.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при добавлении комментария к фотографии в базу данных!");
            }


        }


        public void DeleteComment(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("DELETE FROM dbo.[Comments] WHERE [Id] = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();

                    var result = command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при удалении комментария к фотографии из базы данных!");
            }

        }


        public IEnumerable<Comment> GetCommentsByPhoto(int photoId)
        {
            List<Comment> comments = new List<Comment>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT [Id], [AccountLogin], [PhotoId], [Date], [Text] FROM dbo.[Comments] WHERE [PhotoId] = @PhotoId", connection);
                    command.Parameters.AddWithValue("@PhotoId", photoId);
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        comments.Add(new Comment((int)reader["Id"], (string)reader["AccountLogin"], (int)reader["PhotoId"],
                                                 (DateTime)reader["Date"], (string)reader["Text"]));
                    }
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при получении комментариев к фотографии из базы данных!");
            }


            foreach (var item in comments)
            {
                yield return item;
            }
        }
    }
}

