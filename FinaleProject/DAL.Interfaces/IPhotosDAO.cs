using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPhotosDAO
    {
        IEnumerable<Photo> GetAllPhotos();

        Photo GetPhoto(int id);

        void AddPhoto(int albumId, string title, string accountLogin, byte[] image);

        bool LikePhoto(int photoId, string login);

        IEnumerable<Photo> GetAllPhotosByAccount(string login);

        IEnumerable<Photo> GetAllPhotosByAlbum(int albumId);

        void DeletePhoto(int id);
    }
}
