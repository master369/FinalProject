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
    public class PhotosLogic : IPhotosBLL
    {
        private IPhotosDAO photosDAO;

        public PhotosLogic()
        {
            photosDAO = DAOContainer.PhotosDAO;
        }


        public void AddPhoto(int albumId, string title, string accountLogin, byte[] image)
        {
            photosDAO.AddPhoto(albumId, title, accountLogin, image);
        }

        public void DeletePhoto(int id)
        {
            photosDAO.DeletePhoto(id);
        }

        public IEnumerable<Photo> GetAllPhotos()
        {
          return photosDAO.GetAllPhotos();
        }

        public IEnumerable<Photo> GetAllPhotosByAccount(string login)
        {
            return photosDAO.GetAllPhotosByAccount(login);
        }

        public Photo GetPhoto(int id)
        {
            return photosDAO.GetPhoto(id);
        }
        public IEnumerable<Photo> GetAllPhotosByAlbum(int albumId)
        {
            return photosDAO.GetAllPhotosByAlbum(albumId);
        }
        public IEnumerable<Photo> GetPhotosByAlbum(int albumId)
        {
            return photosDAO.GetAllPhotos().Where(x => x.AlbumId == albumId);
        }

        public bool LikePhoto(int photoId, string login)
        {
            return photosDAO.LikePhoto(photoId, login);
        }
    }
}
