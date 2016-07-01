using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinaleProject.Models
{
    public class AlbumModel
    {

        public AlbumModel()
        {

        }
        public string Title { get; set; }
        public int Id { get; set; }
        public List<Photo> Photos { get; set; }
        public static int MaxId { get; set; }
        public static int MaxPhotoId { get; set; }

        public static IEnumerable<AlbumModel> GetByAccount(string login)
        {
            return Logic.albumsLogic.GetAllForUser(login).Select(ent => new AlbumModel
            {
                Title = ent.Title,
                Id = ent.Id,
                //Photos = Logic.photosLogic.GetPhotosByAlbum(ent.Id).ToList(),
            });
        }

        public static int AddAlbum(string login, string title)
        {
            return Logic.albumsLogic.AddAlbum(login, title);
        }


        public static void DeleteAlbum(int id)
        {
            Logic.albumsLogic.DeleteAlbum(id);
        }

        public static void AddPhoto(PhotoModel photo)
        {
            byte[] image = new byte[photo.Image.ContentLength];
            photo.Image.InputStream.Read(image, 0, photo.Image.ContentLength);

            Logic.photosLogic.AddPhoto(photo.AlbumId, photo.Title, photo.AccountLogin, image);
        }

        public static IEnumerable<PhotoModel> GetPhotosByAlbum(int albumId)
        {
            var photos = Logic.photosLogic.GetPhotosByAlbum(albumId);

            return PhotoModel.GetAll().Where(x => x.AlbumId == albumId).ToArray();
        }

        public static IEnumerable<AlbumModel> GetAllPhotosByAlbum(int albumId)
        {
            return Logic.albumsLogic.GetAllForAlbum(albumId).Select(ent => new AlbumModel
            {
                Photos = Logic.photosLogic.GetPhotosByAlbum(ent.Id).ToList(),
            });
        }
        public static AlbumModel GetAlbum(int albumId)
        {
            var ent = Logic.albumsLogic.GetAlbum(albumId);
            return new AlbumModel
            {
                Title = ent.Title,
                Id = ent.Id,
            };
        }


        public static void DeletePhoto(int id)
        {
            Logic.photosLogic.DeletePhoto(id);
        }
    }
}