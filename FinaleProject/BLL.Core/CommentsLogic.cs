using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using DAL.Interfaces;

namespace BLL.Core
{
   public class CommentsLogic : ICommentsBLL
    {
        private ICommentsDAO commentsDAO;

        public CommentsLogic()
        {
            commentsDAO = DAOContainer.CommentsDAO;
        }

        public void AddComment(string accountLogin, int photoId, string text)
        {
            commentsDAO.AddComment(accountLogin, photoId, text);
        }

        public void DeleteComment(int id)
        {
            commentsDAO.DeleteComment(id);
        }

        public IEnumerable<Comment> GetCommentsByPhoto(int photoId)
        {
           return commentsDAO.GetCommentsByPhoto(photoId);
        }
    }
}
