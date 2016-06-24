using BLL.Core;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinaleProject.Models
{
    public static class Logic
    {
        public static IAlbumsBLL albumsLogic = new AlbumsLogic();
        public static IAccountsBLL accountsLogic = new AccountsLogic();
        public static ICommentsBLL commentsLogic = new CommentsLogic();
        public static IPhotosBLL photosLogic = new PhotosLogic();
    }
}