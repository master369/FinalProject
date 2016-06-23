using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
   public class Album
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public string UserLogin { get; set; }
        public IList<Photo> Photos { get; set; }
        public Album(int id, string userLogin, string title, List<Photo> photos)
        {
            Id = id;
            UserLogin = userLogin;
            Title = title;
            Photos = photos;
        }

        public Album(int id, string userLogin, string title)
        {
            Id = id;
            UserLogin = userLogin;
            Title = title;
            Photos = new List<Photo>();
        }
    }
}
