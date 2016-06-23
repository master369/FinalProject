using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICommentsDAO
    {
        void AddComment(string accountLogin, int photoId, string text);

        void DeleteComment(int id);

        IEnumerable<Comment> GetCommentsByPhoto(int photoId);
    }
}
