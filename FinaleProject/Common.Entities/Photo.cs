using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace Common.Entities
{
   public class Photo
    {
        public Photo()
        {
            Id = 0;
            AlbumId = 0;
            Title = "";
            LikesContainer = new List<string>();
            AddDate = DateTime.Now;
        }
        public Photo(int id, int albumId, string title)
        {
            Id = id;
            AlbumId = albumId;
            Title = title;
            LikesContainer = new List<string>();
            AddDate = DateTime.Now;
        }

        public Photo(int id, int albumId, string title, string accountLogin, byte[] image) 
        {
            Id = id;
            AlbumId = albumId;
            Title = title;
            LikesContainer = new List<string>();
            AddDate = DateTime.Now;
            AccountLogin = accountLogin;
            OriginalImage = image;

        }

        public int Id { get; set; }

        public int AlbumId { get; set; }

        public string Title { get; set; }

        public List<string> LikesContainer { get; set; }

        public byte[] OriginalImage { get; set; }

        public byte[] SmallImage { get; set; }

        public DateTime AddDate { get; set; }

        public string AccountLogin { get; set; }

    }
}
