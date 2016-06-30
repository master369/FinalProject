using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
            #region Make_Small_Image
            Image img = ByteToImage(image);
            Size s = new Size();

            s.Width = 400;
            s.Height = 400 * img.Height / img.Width;
            Bitmap holst = new Bitmap(img, s);
            img = (Image)holst;

            #endregion
            OriginalImage = image;
            SmallImage = ImageToByte(img);


        }

        public int Id { get; set; }

        public int AlbumId { get; set; }

        public string Title { get; set; }

        public List<string> LikesContainer { get; set; }

        public byte[] OriginalImage { get; set; }

        public byte[] SmallImage { get; set; }

        public DateTime AddDate { get; set; }

        public string AccountLogin { get; set; }

        private static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        private static Image ByteToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
