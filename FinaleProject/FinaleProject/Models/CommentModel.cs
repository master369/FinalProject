using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinaleProject.Models
{
    public class CommentModel
    {

        public CommentModel()
        {

        }
        public CommentModel(int photoId)
        {
            PhotoId = photoId;
        }
        public int Id { get; set; }

        public string AccountLogin { get; set; }

        public int PhotoId { get; set; }

        public DateTime Date { get; set; }

        public string Text { get; set; }

        public static void AddComment(string accountLogin, int photoId, string text)
        {
            Logic.commentsLogic.AddComment(accountLogin, photoId, text);
        }

        public static void DeleteComment(int id)
        {
            Logic.commentsLogic.DeleteComment(id);
        }

        public static IEnumerable<CommentModel> GetCommentsByPhoto(int photoId)
        {
            return Logic.commentsLogic.GetCommentsByPhoto(photoId).Select(ent => new CommentModel
            {
                Text = ent.Text,
                Id = ent.Id,
                Date = ent.Date,
                AccountLogin = ent.AccountLogin,
                PhotoId = photoId,
            });
        }

    }
}