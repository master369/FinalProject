using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
   public interface IAccountsDAO
    {
        bool Add(Account user);
        bool AddRole(string name, string role);
        void DeleteRole(string name, string role);
        Account Get(string name);
        IEnumerable<Account> GetAll();
        void EditPassword(string Login, string OldPassword, string NewPassword);

    }
}
