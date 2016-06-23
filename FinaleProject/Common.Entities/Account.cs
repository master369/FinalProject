using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
   public class Account
    {
        public Account(string name, List<string> roles)
        {
            Name = name;
            Roles = roles;
        }
        public Account(string name, string password, List<string> roles)
        {
            Name = name;
            Password = password;
            Roles = new List<string>();
            Roles = roles;
        }
        public Account()
        {
            Roles = new List<string>();
        }

        public Account(string name, string password)
        {
            Name = name;
            Password = password;
            Roles = new List<string>();
        }
        public string Name { get; set; }

        public string Password { get; set; }

        public List<string> Roles { get; set; }
    }
}

