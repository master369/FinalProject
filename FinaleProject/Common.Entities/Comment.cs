using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
   public class Comment
    {
        public Comment(string accountLogin, int photoId, string text)
        {
            Id = 0;
            AccountLogin = accountLogin;
            Date = DateTime.Now;
            PhotoId = photoId;
            Text = text;
        }
        public Comment(int id, string accountLogin, int photoId, string text)
        {
            Id = id;
            AccountLogin = accountLogin;
            Date = DateTime.Now;
            PhotoId = photoId;
            Text = text;
        }

        public Comment(int id, string accountLogin, int photoId, DateTime date, string text)
        {
            Id = id;
            AccountLogin = accountLogin;
            Date = date;
            PhotoId = photoId;
            Text = text;
        }
        public int Id { get; set; }

        public string AccountLogin { get; set; }

        public int PhotoId { get; set; }

        public DateTime Date { get; set; }

        public string Text { get; set; }
    }
}
