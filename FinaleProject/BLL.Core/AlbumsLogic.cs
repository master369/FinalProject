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
    public class AlbumsLogic : IAlbumsBLL
    {
        private IAlbumsDAO albumsDAO;
       
        public AlbumsLogic()
        {
            albumsDAO = DAOContainer.AlbumsDAO;
        }

        public int AddAlbum(string login, string title)
        {
            return albumsDAO.AddAlbum(login, title);
        }


        public void DeleteAlbum(int id)
        {
            albumsDAO.DeleteAlbum(id);
        }


        public Album GetAlbum(int albumId)
        {
            return albumsDAO.GetAlbum(albumId);
        }

        public IEnumerable<Album> GetAll()
        {
            return albumsDAO.GetAll();
        }
    }
}