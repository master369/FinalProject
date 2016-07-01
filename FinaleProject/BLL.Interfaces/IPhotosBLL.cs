using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
   public interface IPhotosBLL
    {
        IEnumerable<Photo> GetAllPhotos();

        Photo GetPhoto(int id);

        void AddPhoto(int albumId, string title, string accountLogin, byte[] image);

        bool LikePhoto(int photoId, string login);
        IEnumerable<Photo> GetAllPhotosByAlbum(int albumId);
        IEnumerable<Photo> GetAllPhotosByAccount(string login);
        IEnumerable<Photo> GetPhotosByAlbum(int albumId);
        void DeletePhoto(int id);
    }
}
