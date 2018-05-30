using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using VeterinaryClinicManagment.Models;

namespace VeterinaryClinicManagment.Services
{
    public class UserRoleProvider : RoleProvider 
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override bool IsUserInRole(string username, string roleName)
        {
            using(IDal dal = new Dal())
            {
                var user = dal.GetUserById(int.Parse(username));
                if (user == null)
                    return false;
                if (roleName == "Client")
                    return !user.estVeto;
                else if(roleName=="Veterinary")
                    return user.estVeto;
                return false;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            using(IDal dal = new Dal())
            {
                var user = dal.GetUserById(int.Parse(username));
                if (user == null)
                    return new string[] { };
                if (user.estVeto)
                    return new string[] { "Veterinary" };
                else
                    return new string[] { "Client" };
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return new string[] { "Veterinary", "Client" };
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
    }
}