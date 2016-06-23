using Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using FinaleProject.Models;
using System.Text;

namespace FinaleProject.Models
{
    public class AccountModel
    {

        public AccountModel()
        {

        }
        
        public string Name { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
        //public int Rating
        //{
        //    get
        //    {
        //        return Logic.accountsLogic.RatingByAccount(Name);
        //    }
        //}

        public HttpPostedFileBase Image { get; set; }

        public static IEnumerable<AccountModel> GetAll()
        {
            return Logic.accountsLogic.GetAll().Select(ent => new AccountModel
            {
                Name = ent.Name,
                Roles = ent.Roles,
            });
        }

        public static bool Add(AccountModel model)
        {
            var ent = new Account
            {
                Name = model.Name,
                Password = model.Password,
                Roles = new List<string> { "User" },
            };
            return Logic.accountsLogic.Add(ent);
        }

        public static bool AddRole(string userName, string Role)
        {
            return Logic.accountsLogic.AddRole(userName, Role);
        }

        public static void DeleteRole(string userName, string Role)
        {
            Logic.accountsLogic.DeleteRole(userName, Role);
        }

        public static bool CanLogin(string userName, string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                password = GetMd5Hash(md5Hash, password);
            }
            return (Get(userName) != null && Get(userName).Password == password);
        }

        public static AccountModel Get(string name)
        {
            if (name == "") return null;
            var ent = Logic.accountsLogic.Get(name);

            if (ent == null) return null;

            return
                new AccountModel
                {
                    Name = ent.Name,
                    Password = ent.Password,
                    Roles = ent.Roles,
                };
        }


        public static bool EditPassword(string Login, string OldPassword, string NewPassword)
        {
            return Logic.accountsLogic.EditPassword(Login, OldPassword, NewPassword);
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static bool Signup(string login, string password)
        {
            if (Get(login) != null)
            {
                return false;
            }

            using (MD5 md5Hash = MD5.Create())
            {
                password = GetMd5Hash(md5Hash, password);
            }

            Account ent = new Account(login, password);

            Logic.accountsLogic.Add(ent);
            Logic.accountsLogic.AddRole(login, "User");

            return true;
        }


    }
}