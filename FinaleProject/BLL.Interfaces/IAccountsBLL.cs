using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
  public interface IAccountsBLL
    {
        bool Add(Account user);
        bool AddRole(string name, string role);
        void DeleteRole(string name, string role);
        Account Get(string name);
        IEnumerable<Account> GetAll();
        bool EditPassword(string Login, string OldPassword, string NewPassword);
    }
}
