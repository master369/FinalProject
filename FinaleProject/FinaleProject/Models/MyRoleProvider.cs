﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace FinaleProject.Models
{
    public class MyRoleProvider : RoleProvider
    {
        public override string[] GetAllRoles()
        {
            return new[] { "Admin",  "Moderator","User", };
        }

        public override string[] GetRolesForUser(string username)
        {
            {
                var roles = new List<string>();
                if (username == "Admin")
                {
                    roles.Add("Admin");
                }

                if (username == "Admin")
                {
                    roles.Add("User");
                }

                if (username == "Admin")
                {
                    roles.Add("Moderator");
                }

                return roles.ToArray();
            }
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }



        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}