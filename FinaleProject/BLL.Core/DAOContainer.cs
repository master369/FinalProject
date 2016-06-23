using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Core
{
    class DAOContainer
    {
        public static IAccountsDAO AccountDAO { get; private set; }

        public static IAlbumsDAO AlbumsDAO { get; private set; }

        public static IPhotosDAO PhotosDAO { get; private set; }

        public static ICommentsDAO CommentsDAO { get; private set; }

        static DAOContainer()
        {
            var dal = ConfigurationManager.AppSettings["dal"];
            switch (dal)
            {
                case "DataBase":
                    AccountDAO = new DAL.DataBase.AccountsDAO();
                    AlbumsDAO = new DAL.DataBase.AlbumsDAO();
                    PhotosDAO = new DAL.DataBase.PhotosDAO();
                    CommentsDAO = new DAL.DataBase.CommentsDAO();
                    break;

                default:
                    throw new Exception("Ошибка! Должен быть выбран DataBase DAL!");
            }

        }
    }
}

