using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAlbumsDAO
    {

        IEnumerable<Album> GetAll();

        int AddAlbum(string login, string title);

        Album GetAlbum(int albumId);

        void DeleteAlbum(int id);

    }
}
