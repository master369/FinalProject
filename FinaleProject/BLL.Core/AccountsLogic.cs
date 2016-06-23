using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using DAL.Interfaces;
using System.Security.Cryptography;

namespace BLL.Core
{
    public class AccountsLogic : IAccountsBLL
    {
        private List<Account> accounts;
        private IAccountsDAO accountDAO;

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public void AccountLogic()
        {
            accountDAO = DAOContainer.AccountDAO;
            accounts = accountDAO.GetAll().ToList();
        }
        public bool Add(Account user)
        {
            if (accounts.FirstOrDefault(x => x.Name == user.Name) == null)
            {
                accounts.Add(user);
            }

            return accountDAO.Add(user);
        }

        public bool AddRole(string name, string role)
        {
            if (accounts.FirstOrDefault(x => x.Name == name) != null &&
              !accounts.FirstOrDefault(x => x.Name == name).Roles.Contains(role))
            {
                accounts.FirstOrDefault(x => x.Name == name).Roles.Add(role);
                return accountDAO.AddRole(name, role);
            }
            return false;
        }

        public void DeleteRole(string name, string role)
        {
            if (accounts.FirstOrDefault(x => x.Name == name) != null &&
               accounts.FirstOrDefault(x => x.Name == name).Roles.Contains(role))
            {
                accounts.FirstOrDefault(x => x.Name == name).Roles.Remove(role);
            }

            accountDAO.DeleteRole(name, role);
        }

        public bool EditPassword(string Login, string OldPassword, string NewPassword)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                OldPassword = GetMd5Hash(md5Hash, OldPassword);
                NewPassword = GetMd5Hash(md5Hash, NewPassword);
                if (Get(Login).Password == OldPassword)
                {
                    Get(Login).Password = NewPassword;
                    accountDAO.EditPassword(Login, OldPassword,  NewPassword);
                    return true;
                }
            }

            return false;
        }

        public Account Get(string name)
        {
            return accountDAO.Get(name);
        }

        public IEnumerable<Account> GetAll()
        {
            return accountDAO.GetAll();
        }
    }
}
