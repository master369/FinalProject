using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinaleProject.Models
{
    public class PhotoModel
    {
        public PhotoModel()
        {
            if (LikesContainer == null)
                LikesContainer = new List<string>();
        }
        public int Id { get; set; }

        public int AlbumId { get; set; }

        public string Title { get; set; }

        public DateTime AddDate { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public List<string> LikesContainer { get; set; }

        public string AccountLogin { get; set; }

        public static byte[] GetSmallImage(int Id)
        {
            var image = Logic.photosLogic.GetPhoto(Id).SmallImage;
            return image;
        }

        public static byte[] GetOriginalImage(int Id)
        {
            var image = Logic.photosLogic.GetPhoto(Id).OriginalImage;

            return image;
        }

        public static IEnumerable<PhotoModel> GetAll()
        {
            var photos = Logic.photosLogic.GetAllPhotos();
            return photos.Select(ent => new PhotoModel
            {
                AlbumId = ent.AlbumId,
                Title = ent.Title,
                Id = ent.Id,
                AccountLogin = ent.AccountLogin,
                AddDate = ent.AddDate,
                LikesContainer = ent.LikesContainer,
            });
        }
        public static IEnumerable<PhotoModel> GetAllPhotosByAlbum(int albumId)
        {
            var photos = Logic.photosLogic.GetAllPhotosByAlbum(albumId);
            return photos.ToList().Select(ent => new PhotoModel
            {
                Title = ent.Title,
                Id = ent.Id,
                AlbumId = ent.AlbumId,
                AddDate = ent.AddDate,
                AccountLogin = ent.AccountLogin,
            });
        } 
        public static PhotoModel Get(int photoId)
        {
            Photo ent = Logic.photosLogic.GetPhoto(photoId);
            return new PhotoModel
            {
                Id = photoId,
                AlbumId = ent.AlbumId,
                AccountLogin = ent.AccountLogin,
                LikesContainer = ent.LikesContainer,
                AddDate = ent.AddDate,
                Title = ent.Title,
            };
        }



        public static bool LikePhoto(int id, string login)
        {
            return Logic.photosLogic.LikePhoto(id, login);
        }



    }

}