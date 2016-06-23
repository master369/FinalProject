using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
   public interface ICommentsBLL
    {
        void AddComment(string accountLogin, int photoId, string text);

        void DeleteComment(int id);

        IEnumerable<Comment> GetCommentsByPhoto(int photoId);
    }
}
